using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Tests.HelperClasses;

namespace BeestjeOpJeFeestje.Tests.AnimalBookingValidation
{
    public class BookingValidationTests
    {
        private BookingValidationTestHelper _helper;

        [SetUp]
        public void SetUp()
        {
            _helper = new BookingValidationTestHelper();
        }

        [Test]
        public void ValidateBooking_WhenBookingLionAndFarmAnimal_ShouldReturnNomError()
        {
            _helper.SetupAnimals(new List<Animal>
            {
                new() { Id = 1, Name = "Leeuw", Type = new AnimalType { Name = "Jungle" } },
                new() { Id = 2, Name = "Koe", Type = new AnimalType { Name = "Boerderij" } }
            });

            var viewModel = BookingValidationTestHelper.CreateViewModel([1, 2], "2025-01-12", "test@example.com");
            var results = _helper.ValidateModel(viewModel);

            BookingValidationTestHelper.AssertErrorMessage(results, "Nom nom nom");
        }
        
        [Test]
        public void ValidateBooking_WhenBookingPenguinOnWeekend_ShouldReturnWorkweekError()
        {
            _helper.SetupAnimals(new List<Animal>
            {
                new() { Id = 1, Name = "Pinguïn", Type = new AnimalType { Name = "Sneeuw" } }
            });

            var viewModel = BookingValidationTestHelper.CreateViewModel([1], "2025-01-12", "test@example.com");
            var results = _helper.ValidateModel(viewModel);

            BookingValidationTestHelper.AssertErrorMessage(results, "Dieren in pak werken alleen doordeweeks");
        }
        
        [Test]
        public void ValidateBooking_WhenBookingDesertAnimalInWinter_ShouldReturnColdError()
        {
            _helper.SetupAnimals(new List<Animal>
            {
                new() { Id = 1, Name = "Kameel", Type = new AnimalType { Name = "Woestijn" } }
            });

            var viewModel = BookingValidationTestHelper.CreateViewModel([1], "2025-01-12", "test@example.com");
            var results = _helper.ValidateModel(viewModel);

            BookingValidationTestHelper.AssertErrorMessage(results, "Brrrr – Veelste koud");
        }
        
        [Test]
        public void ValidateBooking_WhenBookingSnowAnimalInSummer_ShouldReturnMeltingError()
        {
            _helper.SetupAnimals(new List<Animal>
            {
                new() { Id = 1, Name = "IJsbeer", Type = new AnimalType { Name = "Sneeuw" } }
            });

            var viewModel = BookingValidationTestHelper.CreateViewModel([1], "2025-07-12", "test@example.com");
            var results = _helper.ValidateModel(viewModel);

            BookingValidationTestHelper.AssertErrorMessage(results, "Some People Are Worth Melting For. ~ Olaf");
        }
        
        [Test]
        public void ValidateBooking_WhenRegularCustomerBooksTooManyAnimals_ShouldReturnLimitError()
        {
            _helper.SetupAnimals(new List<Animal>
            {
                new() { Id = 1, Name = "Hond", Type = new AnimalType { Name = "Boerderij" } },
                new() { Id = 2, Name = "Ezel", Type = new AnimalType { Name = "Boerderij" } },
                new() { Id = 3, Name = "Koe", Type = new AnimalType { Name = "Boerderij" } },
                new() { Id = 4, Name = "Eend", Type = new AnimalType { Name = "Boerderij" } },
            });

            var viewModel = BookingValidationTestHelper.CreateViewModel([1, 2, 3, 4, 5], "2025-01-12", "test@example.com");
            var results = _helper.ValidateModel(viewModel);

            BookingValidationTestHelper.AssertErrorMessage(results, "Kies maximaal drie beestjes");
        }
        
        [Test]
        public void ValidateBooking_WhenSilverCustomerBooksTooManyAnimals_ShouldReturnLimitError()
        {
            _helper.SetupCustomer("test@example.com", "Zilver");
            _helper.SetupAnimals(new List<Animal>
            {
                new() { Id = 1, Name = "Hond", Type = new AnimalType { Name = "Boerderij" } },
                new() { Id = 2, Name = "Ezel", Type = new AnimalType { Name = "Boerderij" } },
                new() { Id = 3, Name = "Koe", Type = new AnimalType { Name = "Boerderij" } },
                new() { Id = 4, Name = "Eend", Type = new AnimalType { Name = "Boerderij" } },
                new() { Id = 5, Name = "Kuiken", Type = new AnimalType { Name = "Boerderij" } }
            });

            var viewModel = BookingValidationTestHelper.CreateViewModel([1, 2, 3, 4, 5], "2025-01-12", "test@example.com");
            var results = _helper.ValidateModel(viewModel);

            BookingValidationTestHelper.AssertErrorMessage(results, "Kies maximaal vier beestjes");
        }
        
        [Test]
        public void ValidateBooking_WhenNonPlatinumCustomerBooksVIPAnimal_ShouldReturnVIPError()
        {
            _helper.SetupCustomer("test@example.com", "Goud");
            _helper.SetupAnimals(new List<Animal>
            {
                new() { Id = 1, Name = "Unicorn", Type = new AnimalType { Name = "VIP" } }
            });

            var viewModel = BookingValidationTestHelper.CreateViewModel([1], "2025-01-12", "test@example.com");
            var results = _helper.ValidateModel(viewModel);

            BookingValidationTestHelper.AssertErrorMessage(results, "Magische ervaring alleen voor VIP's");
        }
    }
}
