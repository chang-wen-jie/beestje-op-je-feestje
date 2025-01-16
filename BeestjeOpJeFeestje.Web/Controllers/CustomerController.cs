using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CustomerController(ICustomerRepository customerRepository, ICustomerTypeRepository customerTypeRepository, UserManager<Customer> userManager,
        IPasswordGeneratorService passwordGeneratorService) : Controller
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly ICustomerTypeRepository _customerTypeRepository = customerTypeRepository;
        private readonly UserManager<Customer> _userManager = userManager;
        private readonly IPasswordGeneratorService _passwordGeneratorService = passwordGeneratorService;
        
        public IActionResult Index()
        {
            var customers = _customerRepository.GetAllCustomers();
            var customerViewModels = customers.Select(customer => new CustomerViewModel
            {
                Name = customer.Name,
                EmailAddress = customer.Email ?? string.Empty,
                Type = customer.Type,
            }).ToList();

            return View(customerViewModels);
        }

        [HttpGet]
        public IActionResult Details(string customerEmail)
        {
            var customer = _customerRepository.GetCustomerByEmail(customerEmail);
            if (customer == null) return NotFound();
            
            var customerViewModel = new CustomerViewModel
            {
                Name = customer.Name,
                HouseNumber = customer.HouseNumber,
                ZipCode = customer.ZipCode,
                EmailAddress = customer.Email ?? string.Empty,
                PhoneNumber = customer.PhoneNumber,
                TypeId = customer.TypeId,
                Type = customer.Type,
            };
            
            return View(customerViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customerViewModel = new CustomerViewModel
            {
                Types = _customerTypeRepository.GetCustomerTypes().Select(ct => new SelectListItem
                {
                    Value = ct.Id.ToString(),
                    Text = ct.Name,
                })
            };
            
            return View(customerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                if (!ModelState.IsValid)
                {
                    customerViewModel.Types = _customerTypeRepository.GetCustomerTypes()
                        .Select(ct => new SelectListItem
                        {
                            Value = ct.Id.ToString(),
                            Text = ct.Name
                        })
                        .ToList();

                    return View(customerViewModel);
                }
            }

            var customer = new Customer()
            {
                UserName = customerViewModel.EmailAddress,
                Name = customerViewModel.Name,
                HouseNumber = customerViewModel.HouseNumber,
                ZipCode = customerViewModel.ZipCode,
                Email = customerViewModel.EmailAddress,
                PhoneNumber = customerViewModel.PhoneNumber,
                TypeId = customerViewModel.TypeId,
            };
            
            var generatedPassword = _passwordGeneratorService.GeneratePassword();
            await _userManager.CreateAsync(customer, generatedPassword);
            await _userManager.AddToRoleAsync(customer, "Customer");
            
            TempData["SuccessMessage"] = $"{customer.Name} is aangemaakt met het wachtwoord: {generatedPassword}";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Edit(string customerEmail)
        {
            var customer = _customerRepository.GetCustomerByEmail(customerEmail);
            if (customer == null) return NotFound();

            var customerViewModel = new CustomerViewModel
            {
                Name = customer.Name,
                HouseNumber = customer.HouseNumber,
                ZipCode = customer.ZipCode,
                EmailAddress = customer.Email ?? string.Empty,
                Types = _customerTypeRepository.GetCustomerTypes()
                    .Select(ct => new SelectListItem
                    {
                        Value = ct.Id.ToString(),
                        Text = ct.Name
                    })
                    .ToList(),
            };
            
            return View(customerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                if (!ModelState.IsValid)
                {
                    customerViewModel.Types = _customerTypeRepository.GetCustomerTypes()
                        .Select(ct => new SelectListItem
                        {
                            Value = ct.Id.ToString(),
                            Text = ct.Name
                        })
                        .ToList();

                    return View(customerViewModel);
                }
            }
            
            var customer = await _userManager.FindByEmailAsync(customerViewModel.EmailAddress);
            if (customer == null) return NotFound();

            customer.UserName = customerViewModel.EmailAddress;
            customer.Name = customerViewModel.Name;
            customer.HouseNumber = customerViewModel.HouseNumber;
            customer.ZipCode = customerViewModel.ZipCode;
            customer.Email = customerViewModel.EmailAddress;
            customer.PhoneNumber = customerViewModel.PhoneNumber;
            customer.TypeId = customerViewModel.TypeId;

            var result = await _userManager.UpdateAsync(customer);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = $"{customer.Name} kon niet worden bewerkt";
                return RedirectToAction("Edit", new { customerEmail = customer.Email });
            }

            TempData["SuccessMessage"] = $"{customer.Name} is bewerkt";
            return RedirectToAction("Details", new { customerEmail = customer.Email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string customerEmail)
        {
            var customer = await _userManager.FindByEmailAsync(customerEmail);
            if (customer == null) return NotFound();

            var result = await _userManager.DeleteAsync(customer);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = $"{customer.Name} kon niet worden verwijderd";
                return RedirectToAction("Edit", new { customerEmail });
            }
            
            TempData["SuccessMessage"] = $"{customer.Name} is verwijderd";
            return RedirectToAction("Index");
        }
    }
}
