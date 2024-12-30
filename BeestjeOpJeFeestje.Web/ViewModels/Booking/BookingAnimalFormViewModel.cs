using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingAnimalFormViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Selecteer een beestje om verder te gaan")]
        public List<int> SelectedAnimalIds { get; set; } = [];
        public List<BeestjeOpJeFeestje.Data.Models.Animal>? AvailableAnimals { get; set; }
        public BookingFormStateViewModel? BookingFormState { get; set; }
        public BeestjeOpJeFeestje.Data.Models.Customer? Customer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SelectedAnimalIds.Count == 0)
            {
                yield return new ValidationResult(
                    "Selecteer minimaal één beestje om verder te gaan.",
                    [nameof(SelectedAnimalIds)]);
            }
        }
    }
}