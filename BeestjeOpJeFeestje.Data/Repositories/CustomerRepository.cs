using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class CustomerRepository(BeestjeOpJeFeestjeDbContext context) : ICustomerRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IQueryable<Customer> GetAllCustomers()
    {
        return _context.Users.Include(c => c.Type);
    }
    
    public Customer? GetCustomerByEmail(string email)
    {
        var customerToRead = _context.Users
            .FirstOrDefault(c => c.Email == email);
        return customerToRead;
    }
}