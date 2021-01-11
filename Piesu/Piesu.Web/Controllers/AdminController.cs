using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piesu.Web.Areas.Identity.Data;
using Piesu.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Piesu.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IdentityDataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IdentityDataContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Breed()
        {
            var breeds = _dbContext.Breeds.ToList();
            return View(breeds);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult User()
        {
            var allUsers = _userManager.Users.ToList();
            var moderators = _userManager.GetUsersInRoleAsync("Moderator").Result;
            var users = allUsers.Except(moderators).ToList();
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Moderator()
        {
            var moderators = _userManager.GetUsersInRoleAsync("Moderator").Result.ToList();
            return View(moderators);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}