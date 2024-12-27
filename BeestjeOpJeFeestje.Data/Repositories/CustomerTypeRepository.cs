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
    
    public CustomerType? GetCustomerType(int customerTypeId)
    {
        var customerToRead = _context.CustomerTypes.Find(customerTypeId);
        return customerToRead;
    }
}