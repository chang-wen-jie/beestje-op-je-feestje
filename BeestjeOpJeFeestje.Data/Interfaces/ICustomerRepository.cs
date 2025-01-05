using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface ICustomerRepository
{
    public IQueryable<Customer> GetAllCustomers();
    public Customer? GetCustomerByEmail(string email);
}