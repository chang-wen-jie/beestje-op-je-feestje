using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingAnimalFormViewModel
    {
        public DateTime BookingDate { get; set; }
        
        [Required(ErrorMessage = "Selecteer een beestje om verder te gaan")]
        public List<int> SelectedAnimalIds { get; set; }
        public List<BeestjeOpJeFeestje.Data.Models.Animal>? AvailableAnimals { get; set; }
        //public BookingStatusModel? BookingStatus { get; set; }
        public string? LoggedInUserEmail { get; set; }

        /*
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) 
        {
            var customerRepository = validationContext.GetService(typeof(ICustomerRepository)) as ICustomerRepository;
            var animalRepository = validationContext.GetService(typeof(IAnimalRepository)) as IAnimalRepository;
            var animals = animalRepository.GetAnimals();
           
            
            if (SelectedAnimalIds != null)
            {
                var customer = customerRepository.GetUserByMail(LoggedInUserEmail);
                var selectedAnimals = animals.Where(animal => SelectedAnimalIds.Contains(animal.Id));
                

                if (selectedAnimals.Any(animal => animal.Name == "Leeuw" || animal.Name == "IJsbeer"))
                {
                    if (selectedAnimals.Any(animal => animal.Type == "Boerderij"))
                        yield return new ValidationResult("Nom nom nom", new[] { nameof(SelectedAnimalIds) });
                }

                if (selectedAnimals.Any(animal => animal.Name == "Pinguïn"))
                {
                    if (BookingDate.DayOfWeek == DayOfWeek.Saturday || BookingDate.DayOfWeek == DayOfWeek.Sunday)
                        yield return new ValidationResult("Dieren in pak werken alleen doordeweeks!", new[] { nameof(SelectedAnimalIds) });
                }

                if (selectedAnimals.Any(animal => animal.Type == "Woestijn"))
                {
                    if (BookingDate.Month >= 10 || BookingDate.Month <= 2)
                        yield return new ValidationResult("Brrrr – Veelste koud", new[] { nameof(SelectedAnimalIds) });
                }

                if (selectedAnimals.Any(animal => animal.Type == "Sneeuw"))
                {
                    if (BookingDate.Month >= 6 && BookingDate.Month <= 8)
                        yield return new ValidationResult("Some People Are Worth Melting For. ~ Olaf", new[] { nameof(SelectedAnimalIds) });
                }

               if (LoggedInUser != null)
                {
                    if (!LoggedInUser.CustomerCard.Any())
                    {
                        if (selectedAnimals.Count() > 3)
                            yield return new ValidationResult("Je mag maar 3 dieren boeken", new[] { nameof(SelectedAnimalIds) });
                    }
                    else if (LoggedInUser.CustomerCard == "Zilver")
                    {
                        if (selectedAnimals.Count() > 4)
                            yield return new ValidationResult("Je mag maar 3 dieren boeken", new[] { nameof(SelectedAnimalIds) });
                    }

                    if (LoggedInUser.CustomerCard == "Platina") yield break;
                    if (selectedAnimals.Any(animal => animal.Type == "VIP"))
                        yield return new ValidationResult("Je mag geen VIP dieren boeken", new[] { nameof(SelectedAnimalIds) });
                } else
                {
                    if (selectedAnimals.Any(animal => animal.Type == "VIP"))
                        yield return new ValidationResult("Je mag geen VIP dieren boeken", new[] { nameof(SelectedAnimalIds) });
                    if (selectedAnimals.Count() > 3) //max 3 animal
                        yield return new ValidationResult("Je mag maar 3 dieren boeken", new[] { nameof(SelectedAnimalIds) });
                }
            }
        }*/
    }
}