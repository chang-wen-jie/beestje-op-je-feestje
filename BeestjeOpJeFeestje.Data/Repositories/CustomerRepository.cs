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
    
    public Customer? GetCustomerById(int customerId)
    {
        var customerToRead = _context.Users.Find(customerId);
        return customerToRead;
    }

    public Customer? GetCustomerByAddress(int houseNumber, string zipCode)
    {
        var customerToRead = _context.Users
            .FirstOrDefault(c => c.HouseNumber == houseNumber && c.ZipCode == zipCode);
        return customerToRead;
    }

    public void CreateCustomer(Customer customer)
    {
        _context.Users.Add(customer);
        _context.SaveChanges();
    }

    public bool UpdateCustomer(Customer customer)
    {
        var customerToUpdate = _context.Users.Find(customer.Id);
        if (customerToUpdate == null) return false;
        
        _context.Entry(customerToUpdate).CurrentValues.SetValues(customer);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteCustomer(int customerId)
    {
        var customerToDelete = _context.Users.Find(customerId);
        if (customerToDelete == null) return false;
        
        _context.Users.Remove(customerToDelete);
        _context.SaveChanges();
        return true;
    }
}