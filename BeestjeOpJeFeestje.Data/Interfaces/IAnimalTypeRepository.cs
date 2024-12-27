using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface IAnimalTypeRepository
{
    public IEnumerable<AnimalType> GetAnimalTypes();
    public AnimalType? GetAnimalType(int animalTypeId);
}