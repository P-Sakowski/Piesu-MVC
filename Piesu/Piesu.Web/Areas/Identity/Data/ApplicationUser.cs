using Microsoft.AspNetCore.Identity;

namespace Piesu.Web.Areas.Identity.Data
{
    //Our implementation of extended ApplicationUser
    public class ApplicationUser : IdentityUser
    {
        public virtual bool IdentityConfirmed { get; set; }
    }
}
