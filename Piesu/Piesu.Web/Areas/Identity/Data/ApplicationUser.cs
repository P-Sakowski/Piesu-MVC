using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piesu.Web.Areas.Identity.Data
{
    //Our implementation of extended ApplicationUser
    public class ApplicationUser: IdentityUser
    {
        public virtual bool IdentityConfirmed { get; set; }
    }
}
