using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class CustomerRepository(BeestjeOpJeFeestjeDbContext context) : ICustomerRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _context.Customers;
    }
    
    public Customer? GetCustomerById(int customerId)
    {
        var customerToRead = _context.Customers.Find(customerId);
        return customerToRead;
    }

    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public bool UpdateCustomer(Customer customer)
    {
        var customerToUpdate = _context.Customers.Find(customer.Id);
        if (customerToUpdate == null) return false;
        
        _context.Entry(customerToUpdate).CurrentValues.SetValues(customer);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteCustomer(int customerId)
    {
        var customerToDelete = _context.Customers.Find(customerId);
        if (customerToDelete == null) return false;
        
        _context.Customers.Remove(customerToDelete);
        _context.SaveChanges();
        return true;
    }
}