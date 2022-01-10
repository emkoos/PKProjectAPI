using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Controllers
{
    public class TeamsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
