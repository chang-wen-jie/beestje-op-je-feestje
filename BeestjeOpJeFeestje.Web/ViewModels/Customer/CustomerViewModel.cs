using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Web.ViewModels.Customer
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Huisnummer is verplicht")]
        public int HouseNumber { get; set; }
        
        [Required(ErrorMessage = "E-mailadres is verplicht")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "E-mailadres moet een geldige formaat aanhouden")]
        public string EmailAddress { get; set; }
        
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Telefoonnummer moet het internationale of binnenlandse formaat aanhouden")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht")]
        [RegularExpression(@"^\d{4}[A-Za-z]{2}$", ErrorMessage = "Postcode moet de 1234AB-formaat aanhouden")]
        public string ZipCode { get; set; }

        [Range(1, 4, ErrorMessage = "Type moet tussen de 1 en 4 liggen")]
        public int? TypeId { get; set; }
        
        public CustomerType? Type { get; set; }
    }
}
