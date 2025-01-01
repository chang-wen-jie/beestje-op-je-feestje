using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Web.ViewModels.Animal
{
    public class AnimalViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is verplicht")]
        [Range(1, 5, ErrorMessage = "Type moet tussen de 1 en 5 liggen")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Prijs is verplicht")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Afbeelding link is verplicht")]
        public string ImageUrl { get; set; }
        
        public AnimalType Type { get; set; }
    }
}
