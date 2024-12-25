using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class AccountController(IAccountRepository accountRepository) : Controller
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        
        public IActionResult Index()
        {
            var accounts = _accountRepository.GetAccounts();

            var accountViewModels = accounts.Select(accountViewModel => new AccountViewModel
            {
                Id = accountViewModel.Id,
                EmailAddress = accountViewModel.EmailAddress,
                HouseNumber = accountViewModel.HouseNumber,
                Name = accountViewModel.Name,
                Password = accountViewModel.Password,
                PhoneNumber = accountViewModel.PhoneNumber,
                ZipCode = accountViewModel.ZipCode,
                TypeId = accountViewModel.TypeId,
            }).ToList();
            
            return View(accountViewModels);
        }

        [HttpGet]
        public IActionResult Details(int accountId)
        {
            var account = _accountRepository.GetAccount(accountId);
            if (account == null) return NotFound();

            var accountViewModel = new AccountViewModel()
            {
                Id = account.Id,
                Password = account.Password,
                Name = account.Name,
                HouseNumber = account.HouseNumber,
                ZipCode = account.ZipCode,
                EmailAddress = account.EmailAddress,
                PhoneNumber = account.PhoneNumber,
                TypeId = account.TypeId,
            };
            
            return View(accountViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AccountViewModel accountViewModel)
        {
            if (!ModelState.IsValid) return View(accountViewModel);

            var account = new Account()
            {
                Password = accountViewModel.Password,
                Name = accountViewModel.Name,
                HouseNumber = accountViewModel.HouseNumber,
                ZipCode = accountViewModel.ZipCode,
                EmailAddress = accountViewModel.EmailAddress,
                PhoneNumber = accountViewModel.PhoneNumber,
                TypeId = accountViewModel.TypeId,
            };
            
            _accountRepository.AddAccount(account);
            TempData["SuccessMessage"] = $"{account.Name} is aangemaakt";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int accountId)
        {
            var account = _accountRepository.GetAccount(accountId);
            if (account == null) return NotFound();

            var accountViewModel = new AccountViewModel()
            {
                Id = account.Id,
                Password = account.Password,
                Name = account.Name,
                HouseNumber = account.HouseNumber,
                ZipCode = account.ZipCode,
                EmailAddress = account.EmailAddress,
                PhoneNumber = account.PhoneNumber,
                TypeId = account.TypeId,
            };
            
            return View(accountViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AccountViewModel accountViewModel)
        {
            if (!ModelState.IsValid) return View(accountViewModel);

            var account = new Account()
            {
                Id = accountViewModel.Id,
                Password = accountViewModel.Password,
                Name = accountViewModel.Name,
                HouseNumber = accountViewModel.HouseNumber,
                ZipCode = accountViewModel.ZipCode,
                EmailAddress = accountViewModel.EmailAddress,
                PhoneNumber = accountViewModel.PhoneNumber,
                TypeId = accountViewModel.TypeId,
            };

            if (!_accountRepository.UpdateAccount(account))
            {
                TempData["ErrorMessage"] = $"{account.Name} kon niet worden bewerkt";
                return RedirectToAction("Edit", new { accountId = account.Id });
            }

            TempData["SuccessMessage"] = $"{account.Name} is bewerkt";
            return RedirectToAction("Details", new { accountId = account.Id });
        }

        [HttpPost]
        public IActionResult Delete(int accountId)
        {
            var account = _accountRepository.GetAccount(accountId);
            if (account == null) return NotFound();

            if (!_accountRepository.DeleteAccount(accountId))
            {
                TempData["ErrorMessage"] = $"{account.Name} kon niet worden verwijderd";
                return RedirectToAction("Edit", new { accountId });
            }
            
            TempData["SuccessMessage"] = $"{account.Name} is verwijderd";
            return RedirectToAction("Index");
        }
    }
}
