using System.ComponentModel.DataAnnotations;

namespace Piesu.Web.Models
{
    public class DogViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BirthYear { get; set; }

        [Display(Name = "Breed")]
        public string Breed { get; set; }
    }
}
