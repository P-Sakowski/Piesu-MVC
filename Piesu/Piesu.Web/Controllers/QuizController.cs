using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piesu.Web.Models;
using System.Diagnostics;

namespace Piesu.Web.Controllers
{
    public class QuizController : Controller
    {
        [AllowAnonymous]
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