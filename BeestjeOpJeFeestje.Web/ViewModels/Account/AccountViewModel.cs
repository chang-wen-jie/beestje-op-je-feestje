using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Account
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Huisnummer is verplicht")]
        public int HouseNumber { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht")]
        [RegularExpression(@"^\d{4}[A-Za-z]{2}$", ErrorMessage = "Postcode moet de 1234AB-formaat aanhouden")]
        public string ZipCode { get; set; }

        public string? EmailAddress { get; set; }

        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Telefoonnummer moet het internationale of binnenlandse formaat aanhouden.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Type is verplicht")]
        [Range(1, 4, ErrorMessage = "Type moet tussen de 1 en 4 liggen")]
        public int TypeId { get; set; }
    }
}
