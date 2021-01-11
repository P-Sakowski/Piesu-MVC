using Microsoft.AspNetCore.Mvc;
using Piesu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Piesu.Web.Areas.Identity.Data;
using Piesu.Web.Data;
using Piesu.Web.Entities;

namespace Piesu.Web.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class DogApiController : ControllerBase
    {
        private readonly IdentityDataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DogApiController(IdentityDataContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        
        [HttpPost]
        public AddNewDogResponse Post([FromBody] JsonElement dogJson)
        {
            bool requestSuccessful;
            var currentUserId = _userManager.GetUserId(User); 
            var data = JObject.Parse(dogJson.ToString());
            var entity = new DogEntity
            {
                Name = (string) data["name"],
                Description = (string) data["description"],
                BreedId = (string) data["breed"],
                UserId = currentUserId
            };

            try
            {
                _dbContext.Dogs.Add(entity);
                _dbContext.SaveChanges();
                requestSuccessful = true;
            }
            catch
            {
                requestSuccessful = false;
            }

            AddNewDogResponse result = new AddNewDogResponse
            {
                IsSuccessful = requestSuccessful
            };

            return result;
        }
    }
}