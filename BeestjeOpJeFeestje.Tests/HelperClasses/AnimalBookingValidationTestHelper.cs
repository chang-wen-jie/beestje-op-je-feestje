using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Tests.Mocks;
using BeestjeOpJeFeestje.Web.ViewModels.Booking;
using Moq;

namespace BeestjeOpJeFeestje.Tests.HelperClasses
{
    public class AnimalBookingValidationTestHelper
    {
        private Mock<IAnimalRepository> AnimalRepositoryMock { get; } = new();
        private Mock<ICustomerRepository> CustomerRepositoryMock { get; } = new();

        public List<ValidationResult> ValidateModel(BookingAnimalFormViewModel viewModel)
        {
            var validationContext = new ValidationContext(viewModel, new MockServiceProvider(AnimalRepositoryMock.Object, CustomerRepositoryMock.Object), null);
            return viewModel.Validate(validationContext).ToList();
        }

        public static BookingAnimalFormViewModel CreateViewModel(List<int> selectedAnimalIds, string date, string customerEmail)
        {
            return new BookingAnimalFormViewModel
            {
                SelectedAnimalIds = selectedAnimalIds,
                BookingFormState = new BookingFormStateViewModel { Date = date },
                CustomerEmail = customerEmail
            };
        }
        
        public void SetupAnimals(IEnumerable<Animal> animals)
        {
            AnimalRepositoryMock.Setup(repo => repo.GetAllAnimals()).Returns(animals.AsQueryable());
        }

        public void SetupCustomer(string email, int? typeId, string customerType)
        {
            CustomerRepositoryMock.Setup(repo => repo.GetCustomerByEmail(email)).Returns(new Customer
            {
                TypeId = typeId,
                Type = new CustomerType { Name = customerType }
            });
        }
        
        public static void AssertErrorMessage(List<ValidationResult> results, string expectedMessage)
        {
            Assert.That(results, Has.Count.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo(expectedMessage));
        }
    }
}