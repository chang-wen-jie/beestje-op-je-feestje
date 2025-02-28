﻿    using System.ComponentModel.DataAnnotations;
    using BeestjeOpJeFeestje.Data.Interfaces;
    using BeestjeOpJeFeestje.Web.ViewModels.Animal;

    namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
    {
        public class BookingAnimalFormViewModel : IValidatableObject
        {
            [Required(ErrorMessage = "Selecteer een beestje om verder te gaan")]
            public List<int> SelectedAnimalIds { get; init; } = [];
            public List<AnimalViewModel>? AvailableAnimals { get; set; }
            public BookingFormStateViewModel? BookingFormState { get; set; }
            public string? CustomerEmail { get; init; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (SelectedAnimalIds.Count == 0)
                {
                    yield return new ValidationResult(
                        "Kies minimaal één beestje",
                        [nameof(SelectedAnimalIds)]);
                    yield break;
                }
                
                // Retrieve all animals because AvailableAnimals is empty upon entering Validate
                var animalRepository = validationContext.GetService(typeof(IAnimalRepository)) as IAnimalRepository;
                var customerRepository = validationContext.GetService(typeof(ICustomerRepository)) as ICustomerRepository;
                var animals = animalRepository?.GetAllAnimals();
                var customer = customerRepository?.GetCustomerByEmail(CustomerEmail ?? string.Empty);
                
                var bookingDate = DateOnly.Parse(BookingFormState.Date);
                var selectedAnimals = (animals ?? throw new InvalidOperationException()).Where(animal => SelectedAnimalIds.Contains(animal.Id));
                
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

                var isVipAnimalSelected = selectedAnimals.Any(animal => animal.Type.Name == "VIP");
                if (customer != null)
                {
                    if (customer.Type?.Name != "Platina" && isVipAnimalSelected)
                    {
                        yield return new ValidationResult("Magische ervaring alleen voor VIP's", [nameof(SelectedAnimalIds)
                        ]);
                    }
                    
                    if (customer.Type?.Name == "Zilver" && SelectedAnimalIds.Count > 4)
                    {
                        yield return new ValidationResult("Kies maximaal vier beestjes", [nameof(SelectedAnimalIds)]);
                        yield break; // Ensure statement exit for proper BookingValidationTest testing
                    }
                    
                    if (customer.TypeId == null && SelectedAnimalIds.Count > 3)
                    {
                        yield return new ValidationResult("Kies maximaal drie beestjes", [nameof(SelectedAnimalIds)]);
                    }
                }
                else
                {
                    if (isVipAnimalSelected)
                    {
                        yield return new ValidationResult("Magische ervaring alleen voor VIP's", [nameof(SelectedAnimalIds)
                        ]);
                    }
                    
                    if (SelectedAnimalIds.Count > 3)
                    {
                        yield return new ValidationResult("Kies maximaal drie beestjes", [nameof(SelectedAnimalIds)]);
                    }
                }
            }
        }
    }