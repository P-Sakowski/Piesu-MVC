using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Piesu.Web.Areas.Identity.Data;
using Piesu.Web.Entities;
using Piesu.Web.Models;
using System.Text.Json;

namespace Piesu.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreedApiController : ControllerBase
    {
        private readonly IdentityDataContext _dbContext;

        public BreedApiController(IdentityDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public AddNewBreedResponse Post([FromBody] JsonElement breedJson)
        {
            bool requestSuccessful;
            var data = JObject.Parse(breedJson.ToString());
            var entity = new BreedEntity
            {
                Name = (string)data["name"],
            };

            try
            {
                _dbContext.Breeds.Add(entity);
                _dbContext.SaveChanges();
                requestSuccessful = true;
            }
            catch
            {
                requestSuccessful = false;
            }

            AddNewBreedResponse result = new AddNewBreedResponse
            {
                IsSuccessful = requestSuccessful
            };

            return result;
        }

        [HttpDelete]
        public DeleteBreedResponse Delete([FromBody] JsonElement breedJson)
        {
            bool requestSuccessful;
            var data = JObject.Parse(breedJson.ToString());
            var id = (int)data["id"];
            var entity = _dbContext.Breeds.Find(id);

            try
            {
                _dbContext.Breeds.Remove(entity);
                _dbContext.SaveChanges();
                requestSuccessful = true;
            }
            catch
            {
                requestSuccessful = false;
            }

            DeleteBreedResponse result = new DeleteBreedResponse
            {
                IsSuccessful = requestSuccessful
            };

            return result;
        }
    }
}