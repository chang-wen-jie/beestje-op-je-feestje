using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface IAnimalRepository
{
    public IEnumerable<Animal> GetAnimals();
    public Animal? GetAnimal(int animalId);
    public void AddAnimal(Animal animal);
    public bool UpdateAnimal(Animal animal);
    public bool DeleteAnimal(int animalId);
}