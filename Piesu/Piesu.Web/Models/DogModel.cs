using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piesu.Web.Models
{
    public class DogModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BirthYear { get; set; }
        //to-do: collection of breeds, possible to choose from list while adding new dog
        public string Breed { get; set; }
    }
}
