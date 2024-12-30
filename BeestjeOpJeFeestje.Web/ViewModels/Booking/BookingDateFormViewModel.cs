using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingDateFormViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Datum is verplicht")]
        public DateTime BookingDate { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var bookingDate = DateOnly.FromDateTime(BookingDate);
            var todaysDate = DateOnly.FromDateTime(DateTime.Today);

            if (bookingDate < todaysDate)
            {
                yield return new ValidationResult(
                    "Boekingsdatum kan niet eerder dan vandaag zijn",
                    [nameof(BookingDate)]
                );
            }
        }
    }
}
