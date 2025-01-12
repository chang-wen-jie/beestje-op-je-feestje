using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Booking;
using Moq;

namespace BeestjeOpJeFeestje.Tests;

[TestFixture]
public class BookingAnimalFormTests
{
    private Mock<IAnimalRepository> _animalRepositoryMock;
    private Mock<ICustomerRepository> _customerRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        _animalRepositoryMock = new Mock<IAnimalRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
    }

    private BookingAnimalFormViewModel CreateViewModel(List<int> selectedAnimalIds, string bookingDate,
        string customerEmail)
    {
        return new BookingAnimalFormViewModel
        {
            SelectedAnimalIds = selectedAnimalIds,
            BookingFormState = new BookingFormStateViewModel
            {
                Date = bookingDate,
            },
            CustomerEmail = customerEmail,
        };
    }

    private List<ValidationResult> ValidateModel(BookingAnimalFormViewModel viewModel)
    {
        var context = new ValidationContext(viewModel)
        {
            Items =
            {
                { typeof(IAnimalRepository), _animalRepositoryMock.Object },
                { typeof(ICustomerRepository), _customerRepositoryMock.Object }
            }
        };

        var results = new List<ValidationResult>();
        Validator.TryValidateObject(viewModel, context, results, true);
        return results;
    }

    [Test]
    public void Validate_WhenBookingLionAndFarmAnimal_ReturnsNomNomNomError()
    {
        _animalRepositoryMock.Setup(repo => repo.GetAllAnimals()).Returns(new List<Animal>
        {
            new Animal { Id = 1, Name = "Leeuw", Type = new AnimalType { Name = "Jungle" } },
            new Animal { Id = 2, Name = "Koe", Type = new AnimalType { Name = "Boerderij" } }
        }.AsQueryable());

        var viewModel = CreateViewModel(new List<int> { 1, 2 }, "2025-01-12", "test@example.com");
        
        var results = ValidateModel(viewModel);

        Assert.That(results.Count, Is.EqualTo(1));
        Assert.That(results[0].ErrorMessage, Is.EqualTo("Nom nom nom"));
    }

    [Test]
    public void Validate_WhenBookingPenguinOnWeekend_ReturnsWorkWeekError()
    {
        _animalRepositoryMock.Setup(repo => repo.GetAllAnimals()).Returns(new List<Animal>
        {
            new Animal { Id = 1, Name = "Pinguïn", Type = new AnimalType { Name = "Sneeuw" } }
        }.AsQueryable());
        
        var viewModel = CreateViewModel(new List<int> { 1 }, "2025-01-12", "test@example.com");
        
        var results = ValidateModel(viewModel);
        
        Assert.That(results.Count, Is.EqualTo(1));
        Assert.That(results[0].ErrorMessage, Is.EqualTo("Dieren in pak werken alleen doordeweeks"));
    }
    
    [Test]
    public void Validate_WhenBookingDesertAnimalInWinter_ReturnsTooColdError()
    {
        _animalRepositoryMock.Setup(repo => repo.GetAllAnimals()).Returns(new List<Animal>
        {
            new Animal { Id = 1, Name = "Kameel", Type = new AnimalType { Name = "Woestijn" } }
        }.AsQueryable());

        var viewModel = CreateViewModel(new List<int> { 1 }, "2025-01-12", "test@example.com");

        var results = ValidateModel(viewModel);

        Assert.That(results.Count, Is.EqualTo(1));
        Assert.That(results[0].ErrorMessage, Is.EqualTo("Brrrr – Veelste koud"));
    }
    
    [Test]
    public void Validate_WhenBookingSnowAnimalInSummer_ReturnsMeltingError()
    {
        _animalRepositoryMock.Setup(repo => repo.GetAllAnimals()).Returns(new List<Animal>
        {
            new Animal { Id = 1, Name = "IJsbeer", Type = new AnimalType { Name = "Sneeuw" } }
        }.AsQueryable());

        var viewModel = CreateViewModel(new List<int> { 1 }, "2025-07-12", "test@example.com");

        var results = ValidateModel(viewModel);

        Assert.That(results.Count, Is.EqualTo(1));
        Assert.That(results[0].ErrorMessage, Is.EqualTo("Some People Are Worth Melting For. ~ Olaf"));
    }
    
    [Test]
    public void Validate_WhenNonPlatinumCustomerBooksVIPAnimal_ReturnsVIPError()
    {
        _animalRepositoryMock.Setup(repo => repo.GetAllAnimals()).Returns(new List<Animal>
        {
            new Animal { Id = 1, Name = "Unicorn", Type = new AnimalType { Name = "VIP" } }
        }.AsQueryable());

        _customerRepositoryMock.Setup(repo => repo.GetCustomerByEmail("test@example.com")).Returns(new Customer
        {
            Type = new CustomerType { Name = "Gold" }
        });

        var viewModel = CreateViewModel(new List<int> { 1 }, "2025-01-12", "test@example.com");

        var results = ValidateModel(viewModel);

        Assert.That(results.Count, Is.EqualTo(1));
        Assert.That(results[0].ErrorMessage, Is.EqualTo("Magische ervaring alleen voor VIP's"));
    }

    [Test]
    public void Validate_WhenCustomerBooksTooManyAnimals_ReturnsBookingLimitError()
    {
        _customerRepositoryMock.Setup(repo => repo.GetCustomerByEmail("test@example.com")).Returns(new Customer
        {
            Type = new CustomerType { Name = "Silver" }
        });

        _animalRepositoryMock.Setup(repo => repo.GetAllAnimals()).Returns(new List<Animal>
        {
            new Animal { Id = 1, Name = "Hond", Type = new AnimalType { Name = "Boerderij" } },
            new Animal { Id = 2, Name = "Ezel", Type = new AnimalType { Name = "Boerderij" } },
            new Animal { Id = 3, Name = "Koe", Type = new AnimalType { Name = "Boerderij" } },
            new Animal { Id = 4, Name = "Eend", Type = new AnimalType { Name = "Boerderij" } }
        }.AsQueryable());

        var viewModel = CreateViewModel(new List<int> { 1, 2, 3, 4 }, "2025-01-12", "test@example.com");

        var results = ValidateModel(viewModel);

        Assert.That(results.Count, Is.EqualTo(1));
        Assert.That(results[0].ErrorMessage, Is.EqualTo("Je kunt maximaal 4 beestjes boeken met een zilveren klantenkaart"));
    }
}