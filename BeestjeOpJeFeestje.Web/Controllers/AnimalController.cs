using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    [Authorize]
    public class AnimalController(IAnimalRepository animalRepository, IAnimalTypeRepository animalTypeRepository) : Controller
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IAnimalTypeRepository _animalTypeRepository = animalTypeRepository;
        
        public IActionResult Index()
        {
            var animals = _animalRepository.GetAllAnimals();
            var animalViewModels = animals.Select(animal => new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Price = animal.Price,
                ImageUrl = animal.ImageUrl,
            }).ToList();
            
            return View(animalViewModels);
        }
        
        [HttpGet]
        public IActionResult Details(int animalId)
        {
            var animal = _animalRepository.GetAnimalById(animalId);
            if (animal == null) return NotFound();

            var animalViewModel = new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                TypeId = animal.TypeId,
                Type = animal.Type,
                Price = animal.Price,
                ImageUrl = animal.ImageUrl,
            };
            
            return View(animalViewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var animalViewModel = new AnimalViewModel
            {
                Types = _animalTypeRepository.GetAnimalTypes().Select(at => new SelectListItem
                {
                    Value = at.Id.ToString(),
                    Text = at.Name
                })
            };
            
            return View(animalViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AnimalViewModel animalViewModel)
        {
            if (!ModelState.IsValid)
            {
                animalViewModel.Types = _animalTypeRepository.GetAnimalTypes()
                    .Select(ct => new SelectListItem
                    {
                        Value = ct.Id.ToString(),
                        Text = ct.Name
                    })
                    .ToList();
                
                return View(animalViewModel);
            }

            var animal = new Animal()
            {
                Name = animalViewModel.Name,
                TypeId = animalViewModel.TypeId,
                Price = animalViewModel.Price,
                ImageUrl = animalViewModel.ImageUrl,
            };
            
            _animalRepository.CreateAnimal(animal);
            TempData["SuccessMessage"] = $"{animal.Name} is aangemaakt";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Edit(int animalId)
        {
            var animal = _animalRepository.GetAnimalById(animalId);
            if (animal == null) return NotFound();

            var animalViewModel = new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                TypeId = animal.TypeId,
                Price = animal.Price,
                ImageUrl = animal.ImageUrl,
                Types = _animalTypeRepository.GetAnimalTypes()
                    .Select(ct => new SelectListItem
                    {
                        Value = ct.Id.ToString(),
                        Text = ct.Name
                    })
                    .ToList(),
            };
            
            return View(animalViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AnimalViewModel animalViewModel)
        {
            if (!ModelState.IsValid)
            {
                animalViewModel.Types = _animalTypeRepository.GetAnimalTypes()
                    .Select(ct => new SelectListItem
                    {
                        Value = ct.Id.ToString(),
                        Text = ct.Name
                    })
                    .ToList();
                
                return View(animalViewModel);
            }

            var animal = new Animal()
            {
                Id = animalViewModel.Id,
                Name = animalViewModel.Name,
                TypeId = animalViewModel.TypeId,
                Price = animalViewModel.Price,
                ImageUrl = animalViewModel.ImageUrl,
            };

            if (!_animalRepository.UpdateAnimal(animal))
            {
                TempData["ErrorMessage"] = $"{animal.Name} kon niet worden gewijzigd";
                return RedirectToAction("Edit", new { animalId = animal.Id });
            }

            TempData["SuccessMessage"] = $"{animal.Name} is gewijzigd";
            return RedirectToAction("Details", new { animalId = animal.Id });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int animalId)
        {
            var animal = _animalRepository.GetAnimalById(animalId);
            if (animal == null) return NotFound();

            if (!_animalRepository.DeleteAnimal(animalId))
            {
                TempData["ErrorMessage"] = $"{animal.Name} kon niet worden verwijderd";
                return RedirectToAction("Edit", new { animalId });
            }
            
            TempData["SuccessMessage"] = $"{animal.Name} is verwijderd";
            return RedirectToAction("Index");
        }
    }
}
