using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingDateFormViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Boekingsdatum is verplicht")]
        public DateOnly BookingDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var bookingDate = BookingDate;
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
