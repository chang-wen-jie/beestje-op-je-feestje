using System.Text.Json;
using System.Text.Json.Serialization;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Animal;
using BeestjeOpJeFeestje.Web.ViewModels.Booking;
using BeestjeOpJeFeestje.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class BookingController(IAnimalRepository animalRepository, IBookingRepository bookingRepository, ICustomerRepository customerRepository) : Controller
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BookingDateFormViewModel bookingDateFormViewModel)
        {
            if (!ModelState.IsValid) return View(bookingDateFormViewModel);
            
            var bookingDate = bookingDateFormViewModel.BookingDate.ToShortDateString();
            HttpContext.Session.SetString("BookingDate", bookingDate);
            
            return RedirectToAction("Step1");
        }        
        
        public IActionResult Step1()
        {
            var bookingFormState = GetBookingFormState();
            
            var bookings = _bookingRepository.GetBookings().ToList();
            var animals = _animalRepository.GetAllAnimals().ToList();

            var availableAnimals = animals
                .Where(a => !bookings.Any(b =>
                    b.Date == DateTime.Parse(bookingFormState.Date) && b.Animals.Any(ba => ba.Id == a.Id)))
                .ToList();

            var bookingAnimalFormViewModel = new BookingAnimalFormViewModel
            {
                AvailableAnimals = availableAnimals,
                BookingFormState = bookingFormState,
            };
            
            return View(bookingAnimalFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step1(BookingAnimalFormViewModel bookingAnimalFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var bookingFormState = GetBookingFormState();
                var bookings = _bookingRepository.GetBookings().ToList();
                var animals = _animalRepository.GetAllAnimals().ToList();
                var availableAnimals = animals
                    .Where(a => !bookings.Any(b =>
                        b.Date == DateTime.Parse(bookingFormState.Date) && b.Animals.Any(ba => ba.Id == a.Id)))
                    .ToList();
                
                bookingAnimalFormViewModel.AvailableAnimals = availableAnimals;
                bookingAnimalFormViewModel.BookingFormState = bookingFormState;
                
                return View(bookingAnimalFormViewModel);
            }
            
            var animalIds = bookingAnimalFormViewModel.SelectedAnimalIds;
            var serializedAnimalIds = JsonSerializer.Serialize(animalIds);
            HttpContext.Session.SetString("SelectedAnimalIds", serializedAnimalIds);
            
            return RedirectToAction("Step2");
        }

        public IActionResult Step2()
        {
            var bookingFormState = GetBookingFormState();
            var bookingCustomerFormViewModel = new BookingCustomerFormViewModel
            {
                BookingFormState = bookingFormState,
            };
            
            return View(bookingCustomerFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step2(BookingCustomerFormViewModel bookingCustomerFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var bookingFormState = GetBookingFormState();
                bookingCustomerFormViewModel.BookingFormState = bookingFormState;
                
                return View(bookingCustomerFormViewModel);
            }
            
            HttpContext.Session.SetString("Name", bookingCustomerFormViewModel.Name);
            HttpContext.Session.SetInt32("HouseNumber", bookingCustomerFormViewModel.HouseNumber);
            HttpContext.Session.SetString("ZipCode", bookingCustomerFormViewModel.ZipCode);
            HttpContext.Session.SetString("EmailAddress", bookingCustomerFormViewModel.EmailAddress ?? string.Empty);
            HttpContext.Session.SetString("PhoneNumber", bookingCustomerFormViewModel.PhoneNumber ?? string.Empty);
            return RedirectToAction("Step3");
        }

        public IActionResult Step3()
        {
            var bookingFormState = GetBookingFormState();
            
            var bookingFormViewModel = new BookingFormViewModel()
            {
                TotalPrice = 0,
                BookingFormState = bookingFormState,
            };
            
            return View(bookingFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step3(BookingFormViewModel bookingFormViewModel)
        {
            var bookingFormState = GetBookingFormState();
            var bookingDate = DateTime.Parse(bookingFormState.Date);
            var selectedAnimalIds = bookingFormState.Animals?.Select(a => a.Id).ToList();

            if (bookingFormState.Customer != null) _customerRepository.AddCustomer(bookingFormState.Customer);

            var booking = new Booking
            {
                Date = bookingDate,
                TotalPrice = bookingFormViewModel.TotalPrice,
            };
            
            
            TempData["BookingSuccess"] = "De booking is aangemaakt";
            return RedirectToAction("Index");
        }

        public BookingFormStateViewModel GetBookingFormState()
        {
            var bookingDate = HttpContext.Session.GetString("BookingDate");
            var serializedAnimalIds = HttpContext.Session.GetString("SelectedAnimalIds");
            var name = HttpContext.Session.GetString("Name");
            var houseNumber = HttpContext.Session.GetInt32("HouseNumber");
            var zipCode = HttpContext.Session.GetString("ZipCode");
            var emailAddress = HttpContext.Session.GetString("EmailAddress");
            var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            
            var selectedAnimals = new List<Animal>();
            var customer = new Customer();

            if (!string.IsNullOrEmpty(serializedAnimalIds))
            {
                var selectedAnimalIds = JsonSerializer.Deserialize<List<int>>(serializedAnimalIds, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });

                foreach (var animalid in selectedAnimalIds)
                {
                    var animal = _animalRepository.GetAnimalById(animalid);
                    selectedAnimals.Add(animal);
                }
            }

            var bookingFormState = new BookingFormStateViewModel()
            {
                Date = bookingDate,
                Animals = selectedAnimals,
                Customer = customer,
            };
            
            return bookingFormState;
        }
    }
}
