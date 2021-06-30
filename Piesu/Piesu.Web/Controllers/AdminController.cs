using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piesu.Web.Areas.Identity.Data;
using Piesu.Web.Models;
using System.Diagnostics;
using System.Linq;

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
        [Authorize(Roles = "Admin, Moderator")]
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
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Advert()
        {
            var adverts = _dbContext.Adverts.Where(advert => advert.IsActive)
                .Where(advert => !advert.IsVerified)
                .OrderBy(advert => advert.CreatedDate)
                .Select(advert => new AdvertViewModel
                {
                    Title = advert.Title,
                    Description = advert.Description,
                    CreatedDate = advert.CreatedDate.ToString(),
                    DogName = _dbContext.Dogs.First(dog => dog.Id.ToString() == advert.DogId).Name
                }).ToList();

            return View(adverts);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Users()
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