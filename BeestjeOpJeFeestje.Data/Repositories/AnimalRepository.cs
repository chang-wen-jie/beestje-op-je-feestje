using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class AnimalRepository(BeestjeOpJeFeestjeDbContext context) : IAnimalRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IEnumerable<Animal> GetAnimals()
    {
        return _context.Animals;
    }
    
    public Animal? GetAnimal(int animalId)
    {
        var animalToRead = _context.Animals.Find(animalId);
        return animalToRead;
    }

    public void AddAnimal(Animal animal)
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

    public bool DeleteAnimal(int animalId)
    {
        var animalToDelete = _context.Animals.Find(animalId);
        if (animalToDelete == null) return false;
        
        _context.Animals.Remove(animalToDelete);
        _context.SaveChanges();
        return true;
    }
}