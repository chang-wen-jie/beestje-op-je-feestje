using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Animal;
using BeestjeOpJeFeestje.Web.ViewModels.Booking;
using BeestjeOpJeFeestje.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class BookingController(IAnimalRepository animalRepository, IBookingRepository bookingRepository, ICustomerRepository customerRepository, UserManager<Customer> userManager) : Controller
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly UserManager<Customer> _userManager = userManager;

        public IActionResult Step1()
        {
            // Clear previous booking's session
            var session = HttpContext.Session;
            var sessionKeys = session.Keys;

            foreach (var key in sessionKeys)
            {
                session.Remove(key);
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step1(BookingDateFormViewModel bookingDateFormViewModel)
        {
            if (!ModelState.IsValid) return View(bookingDateFormViewModel);
            
            var bookingDate = bookingDateFormViewModel.BookingDate.ToShortDateString();
            HttpContext.Session.SetString("BookingDate", bookingDate);
            
            return RedirectToAction("Step2");
        }        
        
        public IActionResult Step2()
        {
            var bookingFormState = GetBookingFormState(); 
            var bookings = _bookingRepository.GetBookings().ToList();
            var animals = _animalRepository.GetAllAnimals().ToList();
            var animalViewModels = animals.Select(animal => new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                TypeId = animal.TypeId,
                Price = animal.Price,
                ImageUrl = animal.ImageUrl,
            }).ToList();
            var availableAnimals = animalViewModels
                .Where(a => !bookings.Any(b =>
                    b.Date == DateOnly.Parse(bookingFormState.Date) && b.Animals.Any(ba => ba.Id == a.Id)))
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
        public IActionResult Step2(BookingAnimalFormViewModel bookingAnimalFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var bookingFormState = GetBookingFormState();
                var bookings = _bookingRepository.GetBookings().ToList();
                var animals = _animalRepository.GetAllAnimals().ToList();
                var animalViewModels = animals.Select(animal => new AnimalViewModel
                {
                    Id = animal.Id,
                    Name = animal.Name,
                    TypeId = animal.TypeId,
                    Price = animal.Price,
                    ImageUrl = animal.ImageUrl,
                }).ToList();
                var availableAnimals = animalViewModels
                    .Where(a => !bookings.Any(b =>
                        b.Date == DateOnly.Parse(bookingFormState.Date) && b.Animals.Any(ba => ba.Id == a.Id)))
                    .ToList();
                
                bookingAnimalFormViewModel.AvailableAnimals = availableAnimals;
                bookingAnimalFormViewModel.BookingFormState = bookingFormState;
                
                return View(bookingAnimalFormViewModel);
            }
            
            var animalIds = bookingAnimalFormViewModel.SelectedAnimalIds;
            var serializedAnimalIds = JsonSerializer.Serialize(animalIds);
            HttpContext.Session.SetString("SelectedAnimalIds", serializedAnimalIds);
            
            return RedirectToAction("Step3");
        }

        public async Task<IActionResult> Step3()
        {
            var user = await _userManager.GetUserAsync(User);
            var bookingFormState = GetBookingFormState();
            var bookingCustomerFormViewModel = new BookingCustomerFormViewModel
            {
                BookingFormState = bookingFormState,
            };

            if (user == null) return View(bookingCustomerFormViewModel);
            bookingCustomerFormViewModel.Name = user.Name;
            bookingCustomerFormViewModel.HouseNumber = user.HouseNumber;
            bookingCustomerFormViewModel.ZipCode = user.ZipCode;
            bookingCustomerFormViewModel.EmailAddress = user.Email;
            bookingCustomerFormViewModel.PhoneNumber = user.PhoneNumber;
                
            return View(bookingCustomerFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step3(BookingCustomerFormViewModel bookingCustomerFormViewModel)
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
            HttpContext.Session.SetString("EmailAddress", bookingCustomerFormViewModel.EmailAddress);
            HttpContext.Session.SetString("PhoneNumber", bookingCustomerFormViewModel.PhoneNumber ?? string.Empty);
            
            return RedirectToAction("Step4");
        }

        public IActionResult Step4()
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
        public IActionResult Step4(BookingFormViewModel bookingFormViewModel)
        {
            var bookingFormState = GetBookingFormState();
            var selectedAnimalIds = bookingFormState.Animals?.Select(a => a.Id).ToList();
            var customer = bookingFormState.Customer;

            //check of customer eerst bestaat voordat je het erin doet
            // AnimalBooking werkt niet
            var customerModel = new Customer
            {
                Name = customer.Name,
                HouseNumber = customer.HouseNumber,
                ZipCode = customer.ZipCode,
            };
            
            _customerRepository.CreateCustomer(customerModel);
            var customerId = _customerRepository.GetCustomerByAddress(customerModel.HouseNumber, customerModel.ZipCode).Id;

            var booking = new Booking
            {
                Date = DateOnly.Parse(bookingFormState.Date),
                TotalPrice = bookingFormViewModel.TotalPrice,
                TotalDiscountPercentage = 0,
            };
            
            _bookingRepository.AddBooking(booking);
            
            var session = HttpContext.Session;
            var sessionKeys = session.Keys;

            foreach (var key in sessionKeys)
            {
                session.Remove(key);
            }
            
            TempData["SuccessMessage"] = "Boeking is aangemaakt";
            return RedirectToAction("Step1");
        }

        public BookingFormStateViewModel GetBookingFormState()
        {
            var bookingDate = HttpContext.Session.GetString("BookingDate");
            var serializedAnimalIds = HttpContext.Session.GetString("SelectedAnimalIds");
            var name = HttpContext.Session.GetString("Name") ?? string.Empty;
            var houseNumber = HttpContext.Session.GetInt32("HouseNumber") ?? 0;
            var zipCode = HttpContext.Session.GetString("ZipCode") ?? string.Empty;
            var emailAddress = HttpContext.Session.GetString("EmailAddress") ?? string.Empty;
            var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            
            var selectedAnimals = new List<AnimalViewModel>();
            var customer = new CustomerViewModel
            {
                Name = name,
                HouseNumber = houseNumber,
                ZipCode = zipCode,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
            };

            if (!string.IsNullOrEmpty(serializedAnimalIds))
            {
                // Convert animal ID's to animals
                var selectedAnimalIds = JsonSerializer.Deserialize<List<int>>(serializedAnimalIds, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });

                if (selectedAnimalIds != null)
                {
                    var animals = selectedAnimalIds
                        .Select(animalId => _animalRepository.GetAnimalById(animalId))
                        .Where(animal => animal != null)
                        .Select(animal =>
                        {
                            Debug.Assert(animal != null, nameof(animal) + " != null");
                            return new AnimalViewModel
                            {
                                Id = animal.Id,
                                Name = animal.Name,
                                TypeId = animal.TypeId,
                                Price = animal.Price,
                                ImageUrl = animal.ImageUrl,
                            };
                        })
                        .ToList();
                    
                    selectedAnimals.AddRange(animals);
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
