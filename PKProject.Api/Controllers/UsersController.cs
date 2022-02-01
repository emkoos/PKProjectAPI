using Microsoft.AspNetCore.Mvc;
using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using PKProject.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using PKProject.Api.Configuration;
using PKProject.Api.DTO.Users;
using PKProject.Application.Commands.Users;
using PKProject.Application.Queries.Users;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace PKProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager; 
        private readonly JwtConfig _jwtConfig;
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public UsersController(UserManager<IdentityUser> userManager, IOptions<JwtConfig> options, IMediator mediator,  AppDbContext context)
        {
            _userManager = userManager;
            _jwtConfig = options.Value;
            _mediator = mediator;
            _context = context;
        }

        [HttpPost("user/add-to-team")]
        public async Task<IActionResult> AddUserToTeam([FromBody] AddUserToTeamCommand model)
        {
            await _mediator.Send(model);
            return Ok();
        }

        [HttpGet("user/logged-in-user")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.Email);

            if (loggedInUser is null)
            {
                return Unauthorized();
            }

            var model = new GetLoggedInUserQuery
            {
                UserEmail = loggedInUser
            };

            var result = await _mediator.Send(model);

            string base64Photo = "";

            if (result.Photo != null)
            {
                base64Photo = Convert.ToBase64String(result.Photo);
            }

            var returnUser = new GetUserInfoDto
            {
                Email = result.Email,
                Firstname = result.Firstname,
                Lastname = result.Lastname,
                Photo = base64Photo
            };

            return Ok(returnUser);
        }

        [HttpPut("edit-profile")]
        public async Task<IActionResult> EditProfile([FromBody] UpdateUserCommand user)
        {
            var editUser = await _userManager.FindByEmailAsync(user.Email);
            if (editUser is null)
            {
                return NotFound("Not found user with this email.");
            }

            editUser.Email = user.Email;
            editUser.UserName = user.Username;

            await _userManager.UpdateAsync(editUser);

            await _mediator.Send(user);
            return Ok();
        }

        [HttpPost("upload-photo")]
        public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.Email);
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    var user = await _context.Users.Where(x => x.Email == loggedInUser).FirstOrDefaultAsync();
                    user.Photo = fileBytes;
                    _context.Entry(user).CurrentValues.SetValues(user);

                    await _context.SaveChangesAsync();
                }
            }
            return Ok();
        }

        //[HttpPost("download-file/{email}")]
        //public async Task<IActionResult> DownloadPhoto(string email)
        //{
        //    var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

        //    string base64Photo = Convert.ToBase64String(user.Photo);
        //    var returnPhoto = new GetUserPhotoDto
        //    {
        //        Photo = base64Photo
        //    };

        //    return Ok(returnPhoto);
        //}

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResult()
                    {
                        Errors = new List<string>() {
                                "Email already in use"
                            },
                        Success = false
                    });
                }
                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);

                    var newUserModel = new CreateUserCommand
                    {
                        Email = user.Email,
                        Username = user.Username,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Photo = user.Photo
                    };

                    var result = await _mediator.Send(newUserModel);
                    
                    if(result == false)
                    {
                        return BadRequest("Something wrong with adding new user");
                    }

                    return Ok(new RegistrationResult()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResult()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            }

            return BadRequest(new RegistrationResult()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResult()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResult()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new RegistrationResult()
                {
                    Success = true,
                    Token = jwtToken
                });
            }

            return BadRequest(new RegistrationResult()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
