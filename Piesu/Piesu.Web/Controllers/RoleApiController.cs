using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Piesu.Web.Areas.Identity.Data;
using Piesu.Web.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace Piesu.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleApiController : ControllerBase
    {
        private readonly IdentityDataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleApiController(IdentityDataContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<GrantNewRoleResponse> Post([FromBody] JsonElement userIdJson)
        {
            bool requestSuccessful;
            var data = JObject.Parse(userIdJson.ToString());
            var user = _userManager.FindByIdAsync(data["id"].ToString()).Result;
            try
            {
                await _userManager.AddToRoleAsync(user, "Moderator");
                requestSuccessful = true;
            }
            catch
            {
                requestSuccessful = false;
            }

            GrantNewRoleResponse result = new GrantNewRoleResponse
            {
                IsSuccessful = requestSuccessful
            };

            return result;
        }

        [HttpDelete]
        public async Task<RevokeRoleResponse> Delete([FromBody] JsonElement userIdJson)
        {
            bool requestSuccessful;
            var data = JObject.Parse(userIdJson.ToString());
            ApplicationUser user = _userManager.FindByIdAsync((string)data["id"]).Result;

            try
            {
                await _userManager.RemoveFromRoleAsync(user, "Moderator");
                requestSuccessful = true;
            }
            catch
            {
                requestSuccessful = false;
            }

            RevokeRoleResponse result = new RevokeRoleResponse
            {
                IsSuccessful = requestSuccessful
            };

            return result;
        }
    }
}