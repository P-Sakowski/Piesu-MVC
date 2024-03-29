using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Piesu.Web.Areas.Identity.Data;
using Piesu.Web.Entities;
using Piesu.Web.Models;
using System;
using System.Text.Json;

namespace Piesu.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertApiController : ControllerBase
    {
        private readonly IdentityDataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdvertApiController(IdentityDataContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
        public AddNewAdvertResponse Post([FromBody] JsonElement advertJson)
        {
            bool requestSuccessful;
            var currentUserId = _userManager.GetUserId(User);
            var data = JObject.Parse(advertJson.ToString());
            var entity = new AdvertEntity
            {
                Title = (string)data["title"],
                Description = (string)data["description"],
                DogId = (string)data["dog"],
                UserId = currentUserId,
                IsVerified = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            try
            {
                _dbContext.Adverts.Add(entity);
                _dbContext.SaveChanges();
                requestSuccessful = true;
            }
            catch
            {
                requestSuccessful = false;
            }

            AddNewAdvertResponse result = new AddNewAdvertResponse
            {
                IsSuccessful = requestSuccessful
            };

            return result;
        }

        [HttpDelete]
        public DeleteAdvertResponse Delete([FromBody] JsonElement advertson)
        {
            bool requestSuccessful;
            var data = JObject.Parse(advertson.ToString());
            var id = (int)data["id"];
            var entity = _dbContext.Adverts.Find(id);

            try
            {
                _dbContext.Adverts.Remove(entity);
                _dbContext.SaveChanges();
                requestSuccessful = true;
            }
            catch
            {
                requestSuccessful = false;
            }

            DeleteAdvertResponse result = new DeleteAdvertResponse
            {
                IsSuccessful = requestSuccessful
            };

            return result;
        }

        [HttpPut]
        public ApproveAdvertResponse Approve([FromBody] JsonElement advert)
        {
            bool requestSuccessful;
            var data = JObject.Parse(advert.ToString());
            var id = (int)data["id"];
            var entity = _dbContext.Adverts.Find(id);

            try
            {
                entity.IsVerified = true;
                _dbContext.SaveChanges();
                requestSuccessful = true;
            }
            catch
            {
                requestSuccessful = false;
            }

            ApproveAdvertResponse result = new ApproveAdvertResponse
            {
                IsSuccessful = requestSuccessful
            };

            return result;
        }
    }
}