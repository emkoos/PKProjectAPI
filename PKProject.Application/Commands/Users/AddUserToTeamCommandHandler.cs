using MediatR;
using PKProject.Domain.Exceptions.AppExceptions;
using PKProject.Domain.IRepositories;
using PKProject.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Users
{
    public class AddUserToTeamCommandHandler : IRequestHandler<AddUserToTeamCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IEmailSender _emailSender;

        public AddUserToTeamCommandHandler(IUserRepository userRepository, ITeamRepository teamRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _emailSender = emailSender;
        }

        public async Task<bool> Handle(AddUserToTeamCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.UserExist(request.UserEmail))
            {
                throw new NotFoundException("User with that email does not exists.");
            }
            if (!await _teamRepository.TeamExist(request.TeamId))
            {
                throw new NotFoundException("Team does not exists.");
            }
            if (await _userRepository.UserExistInTeam(request.UserEmail, request.TeamId))
            {
                throw new NotAvailableException("User with that email exists in that team.");
            }

            var result = await _userRepository.AddUserToTeam(request.UserEmail, request.TeamId);

            if (result == true)
            {
                var team = await _teamRepository.GetTeamById(request.TeamId);

                string subject = $"Dodano Cię do zespołu {team.Name}.";
                string toUserEmail = request.UserEmail;
                string text = $"Cześć!<br />"+
                                $"Właśnie dodano Cię do zespołu {team.Name} w aplikacji PkProjectApp. <br />" +
                                $"Zobacz zmiany w aplikacji <a href='#'>PkProjectApp</a> <br />" +
                                $"Zespół PkProjectApp <br />"+
                                $"Miłego dnia!";

                await _emailSender.SendEmail(subject, toUserEmail, text);
            }

            return result;
        }
    }
}
