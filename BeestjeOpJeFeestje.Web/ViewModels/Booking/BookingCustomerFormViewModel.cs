using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingCustomerFormViewModel
    {
        [Required(ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Huisnummer is verplicht")]
        public int HouseNumber { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht")]
        [RegularExpression(@"^\d{4}[A-Za-z]{2}$", ErrorMessage = "Postcode moet de 1234AB-formaat aanhouden")]
        public string ZipCode { get; set; }

        public BookingFormStateViewModel? BookingFormState { get; set; }
    }
}
