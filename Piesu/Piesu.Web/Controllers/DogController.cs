using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piesu.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Piesu.Web.Controllers
{
    public class DogController : Controller
    {
        [Authorize(Roles = "Admin, Moderator, User")]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}