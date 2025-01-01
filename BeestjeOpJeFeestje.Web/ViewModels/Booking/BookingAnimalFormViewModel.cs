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
                        "Kies minimaal één beestje om verder te gaan",
                        [nameof(SelectedAnimalIds)]);
                    yield break;
                }
                
                Console.WriteLine("HALLO DAAR W");
                Console.WriteLine("WAT IS DEZE? " + BookingFormState.Date);
                
                var animalRepository = validationContext.GetService(typeof(IAnimalRepository)) as IAnimalRepository;
                var animals = animalRepository.GetAllAnimals();
                var selectedAnimals = animals.Where(animal => SelectedAnimalIds.Contains(animal.Id));

                if (!selectedAnimals.Any(animal => animal.Name == "Leeuw" || animal.Name == "IJsbeer")) yield break;
                    if (selectedAnimals.Any(animal => animal.Type.Name == "Boerderij"))
                        yield return new ValidationResult("Nom nom nom", [nameof(SelectedAnimalIds)]);
                
                if (selectedAnimals.Any(animal => animal.Name == "Pinguïn"))
                    yield return new ValidationResult("Brrrr – Veelste koud", [nameof(SelectedAnimalIds)]);
            }
        }
    }