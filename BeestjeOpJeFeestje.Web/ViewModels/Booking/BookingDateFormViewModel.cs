using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingDateFormViewModel
    {
        [Required(ErrorMessage = "Datum is verplicht")]
        [BookingDateValidation(ErrorMessage = "Boekingsdatum kan niet eerder dan vandaag zijn")]
        public DateTime BookingDate { get; set; }
    }

    public class BookingDateValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Boekingsdatum is verplicht");
            
            var bookingDate = DateOnly.FromDateTime((DateTime)value);
            var todaysDate = DateOnly.FromDateTime(DateTime.Today);
            
            return bookingDate < todaysDate ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }
}
