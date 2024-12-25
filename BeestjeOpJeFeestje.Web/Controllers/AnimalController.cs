using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Web.ViewModels.Animal;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class AnimalController(IAnimalRepository animalRepository) : Controller
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        
        public IActionResult Index()
        {
            var animals = _animalRepository.GetAnimals();

            var animalViewModels = animals.Select(animalViewModel => new AnimalViewModel()
            {
                Id = animalViewModel.Id,
                Name = animalViewModel.Name,
                TypeId = animalViewModel.TypeId,
                Price = animalViewModel.Price,
                ImageUrl = animalViewModel.ImageUrl,
            }).ToList();
            
            return View(animalViewModels);
        }
        
        [HttpGet]
        public IActionResult Details(int animalId)
        {
            var animal = _animalRepository.GetAnimal(animalId);
            if (animal == null) return NotFound();

            var animalViewModel = new AnimalViewModel()
            {
                Id = animal.Id,
                Name = animal.Name,
                TypeId = animal.TypeId,
                Price = animal.Price,
                ImageUrl = animal.ImageUrl,
            };
            
            return View(animalViewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(AnimalViewModel animalViewModel)
        {
            if (!ModelState.IsValid) return View(animalViewModel);

            var animal = new Animal()
            {
                Name = animalViewModel.Name,
                TypeId = animalViewModel.TypeId,
                Price = animalViewModel.Price,
                ImageUrl = animalViewModel.ImageUrl,
            };
            
            _animalRepository.AddAnimal(animal);
            TempData["SuccessMessage"] = $"{animal.Name} is aangemaakt";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Edit(int animalId)
        {
            var animal = _animalRepository.GetAnimal(animalId);
            if (animal == null) return NotFound();

            var animalViewModel = new AnimalViewModel()
            {
                Id = animal.Id,
                Name = animal.Name,
                TypeId = animal.TypeId,
                Price = animal.Price,
                ImageUrl = animal.ImageUrl,
            };
            
            return View(animalViewModel);
        }
        
        [HttpPost]
        public IActionResult Edit(AnimalViewModel animalViewModel)
        {
            if (!ModelState.IsValid) return View(animalViewModel);

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
        public IActionResult Delete(int animalId)
        {
            var animal = _animalRepository.GetAnimal(animalId);
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
