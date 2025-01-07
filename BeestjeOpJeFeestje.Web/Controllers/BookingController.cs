using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Business.Services;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Animal;
using BeestjeOpJeFeestje.Web.ViewModels.Booking;
using BeestjeOpJeFeestje.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class BookingController(IAnimalRepository animalRepository, IBookingRepository bookingRepository,
        ICustomerRepository customerRepository, UserManager<Customer> userManager, IPasswordGeneratorService passwordGeneratorService,
        DiscountService discountService) : Controller
    {
        private readonly UserManager<Customer> _userManager = userManager;
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IPasswordGeneratorService _passwordGeneratorService = passwordGeneratorService;
        private readonly DiscountService _discountService = discountService;

        public IActionResult Index()
        {
            var customerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var bookings = _bookingRepository.GetBookingsByCustomerId(customerId);
            var bookingViewModels = bookings.Select(booking => new BookingViewModel
            {
                Id = booking.Id,
                Date = booking.Date,
                CustomerId = booking.CustomerId,
                TotalPrice = booking.TotalPrice,
                TotalDiscountPercentage = booking.TotalDiscountPercentage,
                Animals = booking.Animals.Select(animal => new AnimalViewModel
                {
                    Name = animal.Name,
                    ImageUrl = animal.ImageUrl,
                }).ToList()
            }).ToList();
            
            return View(bookingViewModels);
        }

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
            var userEmail = HttpContext.User.Identity?.Name;
            var bookings = _bookingRepository.GetAllBookings().ToList();
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
                CustomerEmail = userEmail,
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
                var bookings = _bookingRepository.GetAllBookings().ToList();
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
            var bookingFormState = GetBookingFormState();
            var bookingCustomerFormViewModel = new BookingCustomerFormViewModel
            {
                BookingFormState = bookingFormState,
            };
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return View(bookingCustomerFormViewModel);
            bookingCustomerFormViewModel.Name = user.Name;
            bookingCustomerFormViewModel.HouseNumber = user.HouseNumber;
            bookingCustomerFormViewModel.ZipCode = user.ZipCode;
            bookingCustomerFormViewModel.EmailAddress = user.Email ?? string.Empty;
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
            var bookingFormViewModel = new BookingFormViewModel
            {
                BookingFormState = bookingFormState,
            };

            var customer = new Customer
            {
                TypeId = bookingFormViewModel.BookingFormState.Customer.TypeId,
            };
            
            var booking = new Booking
            {
                Date = DateOnly.Parse(bookingFormState.Date),
                Customer = customer,
                Animals = bookingFormState.Animals.Select(a => new Animal
                {
                    Name = a.Name,
                    TypeId = a.TypeId,
                }).ToList(),
            };
            
            var totalPrice = bookingFormViewModel.BookingFormState.Animals.Sum(a => a.Price);
            var discountPercentage = _discountService.CalculateDiscount(booking);
            var discountedPrice = totalPrice * (1 - discountPercentage / 100);
            bookingFormViewModel.TotalPrice = discountedPrice;
            bookingFormViewModel.TotalDiscountPercentage = discountPercentage;
            
            return View(bookingFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step4(BookingFormViewModel bookingFormViewModel)
        {
            var bookingFormState = GetBookingFormState();
            var customer = bookingFormState.Customer;
            var existingCustomer = _customerRepository.GetCustomerByEmail(customer.EmailAddress);
            var phoneNumber = string.IsNullOrWhiteSpace(customer.PhoneNumber) ? null : customer.PhoneNumber;
            
            string customerId;
            if (existingCustomer != null)
            {
                customerId = existingCustomer.Id;
            }
            else
            {
                var newCustomer = new Customer
                {
                    UserName = customer.EmailAddress,
                    Name = customer.Name,
                    HouseNumber = customer.HouseNumber,
                    ZipCode = customer.ZipCode,
                    Email = customer.EmailAddress,
                    PhoneNumber =  phoneNumber,
                };
                var generatedPassword = _passwordGeneratorService.GeneratePassword();
                await _userManager.CreateAsync(newCustomer, generatedPassword);

                customerId = newCustomer.Id;
                TempData["AccountSuccessMessage"] = "Klantenaccount is aangemaakt met het wachtwoord: " + generatedPassword;
            }

            var bookingAnimals = bookingFormState.Animals.Select(animalViewModel => _animalRepository.GetAllAnimals()
                    .FirstOrDefault(a => a.Id == animalViewModel.Id))
                .OfType<Animal>()
                .ToList();
            
            var totalPrice = bookingAnimals.Sum(a => a.Price);
            var discountPercentage = _discountService.CalculateDiscount(new Booking
            {
                Date = DateOnly.Parse(bookingFormState.Date),
                Animals = bookingAnimals,
                Customer = new Customer
                {
                    TypeId =bookingFormState.Customer.TypeId,
                }
            });
            var discountedPrice = totalPrice * (1 - discountPercentage / 100);
            
            var booking = new Booking
            {
                Date = DateOnly.Parse(bookingFormState.Date),
                CustomerId = customerId,
                TotalPrice = discountedPrice,
                TotalDiscountPercentage = discountPercentage,
                Animals = bookingAnimals,
            };
            _bookingRepository.AddBooking(booking);
            
            var session = HttpContext.Session;
            var sessionKeys = session.Keys;
            foreach (var key in sessionKeys)
            {
                session.Remove(key);
            }
            
            TempData["BookingSuccessMessage"] = "Boeking is aangemaakt";
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
            
            // Convert animal ID's to animals
            var selectedAnimals = new List<AnimalViewModel>();
            if (!string.IsNullOrEmpty(serializedAnimalIds))
            {
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
            
            var existingCustomer = _customerRepository.GetCustomerByEmail(emailAddress);
            var customer = new CustomerViewModel();
            if (existingCustomer != null)
            {
                customer.Name = existingCustomer.Name;
                customer.HouseNumber = existingCustomer.HouseNumber;
                customer.ZipCode = existingCustomer.ZipCode;
                customer.EmailAddress = existingCustomer.Email;
                customer.PhoneNumber = existingCustomer.PhoneNumber;
                customer.TypeId = existingCustomer.TypeId;
            }
            else
            {
                customer.Name = name;
                customer.HouseNumber = houseNumber;
                customer.ZipCode = zipCode;
                customer.EmailAddress = emailAddress;
                customer.PhoneNumber = phoneNumber;
            }
            
            var bookingFormState = new BookingFormStateViewModel()
            {
                Date = bookingDate,
                Animals = selectedAnimals,
                Customer = customer,
            };
            
            return bookingFormState;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int bookingId)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null) return NotFound();
            
            if (!_bookingRepository.DeleteBooking(bookingId))
            {
                TempData["ErrorMessage"] = "Boeking kon niet worden verwijderd";
                return RedirectToAction("Index");
            }
            TempData["SuccessMessage"] = "Boeking is verwijderd";
            return RedirectToAction("Index");
        }
    }
}
