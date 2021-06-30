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
    public class DogController : Controller
    {
        private readonly IdentityDataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DogController(IdentityDataContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin, Moderator, User")]
        public IActionResult Index()
        {
            var dogs = _dbContext.Dogs.Where(dog => dog.UserId == _userManager.GetUserId(User))
                .OrderBy(dog => dog.Name)
                .Select(dog => new DogViewModel
                {
                    Name = dog.Name,
                    Description = dog.Description,
                    BirthYear = dog.BirthYear,
                    Breed = _dbContext.Breeds.FirstOrDefault(breed => breed.Id.ToString() == dog.BreedId).Name
                }).ToList();

            return View(dogs);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator, User")]

        public IActionResult New()
        {
            var breeds = _dbContext.Breeds.Select(breed =>
                new SelectListItem
                {
                    Value = breed.Id.ToString(),
                    Text = breed.Name
                }).ToList();

            ViewData["AvailableBreeds"] = breeds;

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}