using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Piesu.Web.Areas.Identity.Data;
using Piesu.Web.Models;
using System.Diagnostics;
using System.Linq;

namespace Piesu.Web.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IdentityDataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdvertController(IdentityDataContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin, Moderator, User")]
        public IActionResult Index()
        {
            var adverts = _dbContext.Adverts.Where(advert => advert.UserId != _userManager.GetUserId(User))
                .Where(advert => advert.IsActive)
                .Where(advert => advert.IsVerified)
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

        [Authorize(Roles = "Admin, Moderator, User")]
        public IActionResult CurrentUser()
        {
            var adverts = _dbContext.Adverts.Where(advert => advert.UserId == _userManager.GetUserId(User))
                .Where(advert => advert.IsActive)
                .Where(advert => advert.IsVerified)
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

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator, User")]

        public IActionResult New()
        {
            var dogs = _dbContext.Dogs.Where(dog => dog.UserId == _userManager.GetUserId(User))
                .OrderBy(dog => dog.Name)
                .Select(dog => new SelectListItem
                {
                    Text = dog.Name + " - " + _dbContext.Breeds.FirstOrDefault(breed => breed.Id.ToString() == dog.BreedId).Name,
                    Value = dog.Id.ToString()
                }).ToList();

            ViewData["AvailableDogs"] = dogs;

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
