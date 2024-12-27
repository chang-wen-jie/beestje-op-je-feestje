using System.Text.Json;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Animal;
using BeestjeOpJeFeestje.Web.ViewModels.Booking;
using BeestjeOpJeFeestje.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class BookingController(ICustomerRepository customerRepository, IAnimalRepository animalRepository, IBookingRepository bookingRepository) : Controller
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BookingDateFormViewModel bookingDateFormViewModel)
        {
            if (!ModelState.IsValid) return View(bookingDateFormViewModel);
            
            HttpContext.Session.SetString("BookingDate", bookingDateFormViewModel.BookingDate.ToShortDateString());
            return RedirectToAction("Step1");
        }        
        
        public IActionResult Step1()
        {
            var bookingDate = HttpContext.Session.GetString("BookingDate");
            if (string.IsNullOrEmpty(bookingDate)) return RedirectToAction("Index");

            var bookings = _bookingRepository.GetBookings().ToList();
            var animals = _animalRepository.GetAllAnimals().ToList();

            var availableAnimals = animals
                .Where(a => !bookings.Any(b =>
                    b.Date == DateTime.Parse(bookingDate) && b.Animals.Any(ba => ba.Id == a.Id)))
                .ToList();

            var animalSelectViewModel = new BookingAnimalFormViewModel
            {
                AvailableAnimals = availableAnimals,
            };
            
            return View(animalSelectViewModel);
        }

        [HttpPost]
        public IActionResult Step1(BookingAnimalFormViewModel bookingAnimalFormViewModel)
        {
            if (!ModelState.IsValid) return View(bookingAnimalFormViewModel);
            
            var animalIds = JsonSerializer.Serialize(bookingAnimalFormViewModel.SelectedAnimalIds);
            HttpContext.Session.SetString("SelectedAnimalIds", animalIds);
            return RedirectToAction("Step2");
        }

        public IActionResult Step2()
        {
            var bookingCustomerFormViewModel = new BookingCustomerFormViewModel();
            return View(bookingCustomerFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step2(BookingCustomerFormViewModel bookingCustomerFormViewModel)
        {
            if (!ModelState.IsValid) return View(bookingCustomerFormViewModel);
            
            HttpContext.Session.SetString("Name", bookingCustomerFormViewModel.Name);
            HttpContext.Session.SetInt32("HouseNumber", bookingCustomerFormViewModel.HouseNumber);
            HttpContext.Session.SetString("ZipCode", bookingCustomerFormViewModel.ZipCode);
            HttpContext.Session.SetString("EmailAddress", bookingCustomerFormViewModel.EmailAddress ?? string.Empty);
            HttpContext.Session.SetString("PhoneNumber", bookingCustomerFormViewModel.PhoneNumber ?? string.Empty);
            return RedirectToAction("Step3");
        }

        public IActionResult Step3()
        {
            var bookingDate = HttpContext.Session.GetString("BookingDate");
            var serializedAnimalIds = HttpContext.Session.GetString("SelectedAnimalIds");

            if (bookingDate == null || serializedAnimalIds == null) return RedirectToAction("Index");
            
            var animalIds = JsonSerializer.Deserialize<List<int>>(serializedAnimalIds);
            var selectedAnimals = animalIds.Select(id => _animalRepository.GetAnimalById(id)).ToList();

            var customer = new Customer()
            {
                Name = HttpContext.Session.GetString("Name"),
                HouseNumber = HttpContext.Session.GetInt32("HouseNumber") ?? 0,
                ZipCode = HttpContext.Session.GetString("ZipCode"),
                EmailAddress = HttpContext.Session.GetString("EmailAddress") ?? string.Empty,
                PhoneNumber = HttpContext.Session.GetString("PhoneNumber") ?? string.Empty,
            };

            var totalPrice = selectedAnimals.Sum(a => a.Price);
            var totalPriceAfterDiscount = 0;

            var bookingFormViewModel = new BookingFormViewModel()
            {
                //Date = bookingDate,
                Customer = customer,
                TotalPrice = totalPrice,
            };
            
            return View(bookingFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step3(BookingFormViewModel bookingFormViewModel)
        {
            var bookingDate = HttpContext.Session.GetString("BookingDate");
            var serializedAnimalIds = HttpContext.Session.GetString("SelectedAnimalIds");
            
            var animalIds = JsonSerializer.Deserialize<List<int>>(serializedAnimalIds);
            var selectedAnimals = animalIds.Select(id => _animalRepository.GetAnimalById(id)).ToList();

            var customer = new Customer()
            {
                Name = HttpContext.Session.GetString("Name"),
                HouseNumber = HttpContext.Session.GetInt32("HouseNumber") ?? 0,
                ZipCode = HttpContext.Session.GetString("ZipCode"),
                EmailAddress = HttpContext.Session.GetString("EmailAddress") ?? string.Empty,
                PhoneNumber = HttpContext.Session.GetString("PhoneNumber") ?? string.Empty,
            };
            
            return RedirectToAction("Index");
        }
    }
}
