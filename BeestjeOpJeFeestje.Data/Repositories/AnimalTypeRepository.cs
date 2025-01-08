using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class AnimalTypeRepository(BeestjeOpJeFeestjeDbContext context) : IAnimalTypeRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IEnumerable<AnimalType> GetAnimalTypes()
    {
        return _context.AnimalTypes;
    }
}