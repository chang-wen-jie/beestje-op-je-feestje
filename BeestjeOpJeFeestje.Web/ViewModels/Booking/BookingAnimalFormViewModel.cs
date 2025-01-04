    using System.ComponentModel.DataAnnotations;
    using BeestjeOpJeFeestje.Data.Interfaces;
    using BeestjeOpJeFeestje.Web.ViewModels.Animal;
    using BeestjeOpJeFeestje.Web.ViewModels.Customer;

    namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
    {
        public class BookingAnimalFormViewModel : IValidatableObject
        {
            [Required(ErrorMessage = "Selecteer een beestje om verder te gaan")]
            public List<int> SelectedAnimalIds { get; init; }
            public List<AnimalViewModel>? AvailableAnimals { get; set; }
            public BookingFormStateViewModel? BookingFormState { get; set; }
            public CustomerViewModel? Customer { get; init; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (SelectedAnimalIds.Count == 0)
                {
                    yield return new ValidationResult(
                        "Kies minimaal één beestje",
                        [nameof(SelectedAnimalIds)]);
                    yield break;
                }
                
                var animalRepository = validationContext.GetService(typeof(IAnimalRepository)) as IAnimalRepository;
                var animals = animalRepository.GetAllAnimals();
                var selectedAnimals = animals.Where(animal => SelectedAnimalIds.Contains(animal.Id));
                var bookingDate = DateOnly.Parse(BookingFormState.Date);

                if (selectedAnimals.Any(animal => animal.Name == "Leeuw" || animal.Name == "IJsbeer"))
                {
                    if (selectedAnimals.Any(animal => animal.Type.Name == "Boerderij"))
                    {
                        yield return new ValidationResult("Nom nom nom", [nameof(SelectedAnimalIds)]);
                    }
                }

                if (selectedAnimals.Any(animal => animal.Name == "Pinguïn") &&
                    bookingDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                {
                    yield return new ValidationResult("Dieren in pak werken alleen doordeweeks", [nameof(SelectedAnimalIds)]);
                }

                if (selectedAnimals.Any(animal => animal.Type.Name == "Woestijn") &&
                    bookingDate.Month is >= 10 or <= 2)
                {
                    yield return new ValidationResult("Brrrr – Veelste koud", [nameof(SelectedAnimalIds)]);
                }

                if (selectedAnimals.Any(animal => animal.Type.Name == "Sneeuw") &&
                    bookingDate.Month is >= 6 and <= 8)
                {
                    yield return new ValidationResult("Some People Are Worth Melting For. ~ Olaf", [nameof(SelectedAnimalIds)]);
                }
            }
        }
    }