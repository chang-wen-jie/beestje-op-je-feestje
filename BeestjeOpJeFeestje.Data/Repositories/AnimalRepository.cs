using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class AnimalRepository(BeestjeOpJeFeestjeDbContext context) : IAnimalRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IQueryable<Animal> GetAllAnimals()
    {
        return _context.Animals.Include(a => a.Type);
    }
    
    public Animal? GetAnimalById(int id)
    {
        var animalToRead = _context.Animals.Find(id);
        return animalToRead;
    }

    public void CreateAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        _context.SaveChanges();
    }

    public bool UpdateAnimal(Animal animal)
    {
        var animalToUpdate = _context.Animals.Find(animal.Id);
        if (animalToUpdate == null) return false;
        
        _context.Entry(animalToUpdate).CurrentValues.SetValues(animal);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteAnimal(int id)
    {
        var animalToDelete = _context.Animals.Find(id);
        if (animalToDelete == null) return false;
        
        _context.Animals.Remove(animalToDelete);
        _context.SaveChanges();
        return true;
    }
}