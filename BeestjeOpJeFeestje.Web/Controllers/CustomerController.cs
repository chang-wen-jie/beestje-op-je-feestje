using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class CustomerController(ICustomerRepository customerRepository) : Controller
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        
        public IActionResult Index()
        {
            var customers = _customerRepository.GetAllCustomers();

            var customerViewModels = customers.Select(customerViewModel => new CustomerViewModel()
            {
                Id = customerViewModel.Id,
                EmailAddress = customerViewModel.EmailAddress,
                HouseNumber = customerViewModel.HouseNumber,
                Name = customerViewModel.Name,
                Password = customerViewModel.Password,
                PhoneNumber = customerViewModel.PhoneNumber,
                ZipCode = customerViewModel.ZipCode,
                TypeId = customerViewModel.TypeId,
            }).ToList();
            
            return View(customerViewModels);
        }

        [HttpGet]
        public IActionResult Details(int customerId)
        {
            var customer = _customerRepository.GetCustomerById(customerId);
            if (customer == null) return NotFound();

            var customerViewModel = new CustomerViewModel()
            {
                Id = customer.Id,
                Password = customer.Password,
                Name = customer.Name,
                HouseNumber = customer.HouseNumber,
                ZipCode = customer.ZipCode,
                EmailAddress = customer.EmailAddress,
                PhoneNumber = customer.PhoneNumber,
                TypeId = customer.TypeId,
            };
            
            return View(customerViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return View(customerViewModel);

            var customer = new Customer()
            {
                Password = customerViewModel.Password,
                Name = customerViewModel.Name,
                HouseNumber = customerViewModel.HouseNumber,
                ZipCode = customerViewModel.ZipCode,
                EmailAddress = customerViewModel.EmailAddress,
                PhoneNumber = customerViewModel.PhoneNumber,
                TypeId = customerViewModel.TypeId,
            };
            
            _customerRepository.AddCustomer(customer);
            TempData["SuccessMessage"] = $"{customer.Name} is aangemaakt";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int customerId)
        {
            var customer = _customerRepository.GetCustomerById(customerId);
            if (customer == null) return NotFound();

            var customerViewModel = new CustomerViewModel()
            {
                Id = customer.Id,
                Password = customer.Password,
                Name = customer.Name,
                HouseNumber = customer.HouseNumber,
                ZipCode = customer.ZipCode,
                EmailAddress = customer.EmailAddress,
                PhoneNumber = customer.PhoneNumber,
                TypeId = customer.TypeId,
            };
            
            return View(customerViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return View(customerViewModel);

            var customer = new Customer()
            {
                Id = customerViewModel.Id,
                Password = customerViewModel.Password,
                Name = customerViewModel.Name,
                HouseNumber = customerViewModel.HouseNumber,
                ZipCode = customerViewModel.ZipCode,
                EmailAddress = customerViewModel.EmailAddress,
                PhoneNumber = customerViewModel.PhoneNumber,
                TypeId = customerViewModel.TypeId,
            };

            if (!_customerRepository.UpdateCustomer(customer))
            {
                TempData["ErrorMessage"] = $"{customer.Name} kon niet worden bewerkt";
                return RedirectToAction("Edit", new { customerId = customer.Id });
            }

            TempData["SuccessMessage"] = $"{customer.Name} is bewerkt";
            return RedirectToAction("Details", new { customerId = customer.Id });
        }

        [HttpPost]
        public IActionResult Delete(int customerId)
        {
            var customer = _customerRepository.GetCustomerById(customerId);
            if (customer == null) return NotFound();

            if (!_customerRepository.DeleteCustomer(customerId))
            {
                TempData["ErrorMessage"] = $"{customer.Name} kon niet worden verwijderd";
                return RedirectToAction("Edit", new { customerId });
            }
            
            TempData["SuccessMessage"] = $"{customer.Name} is verwijderd";
            return RedirectToAction("Index");
        }
    }
}
