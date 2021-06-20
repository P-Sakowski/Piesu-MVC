using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piesu.Web.Models
{
    public class AdvertModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DogId { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
    }
}
