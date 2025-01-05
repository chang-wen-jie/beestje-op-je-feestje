using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface IAnimalRepository
{
    public IQueryable<Animal> GetAllAnimals();
    public Animal? GetAnimalById(int id);
    public void CreateAnimal(Animal animal);
    public bool UpdateAnimal(Animal animal);
    public bool DeleteAnimal(int id);
}