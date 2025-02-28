using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class CustomerTypeRepository(BeestjeOpJeFeestjeDbContext context) : ICustomerTypeRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IEnumerable<CustomerType> GetCustomerTypes()
    {
        return _context.CustomerTypes;
    }
}