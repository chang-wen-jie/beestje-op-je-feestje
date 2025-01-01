using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface ICustomerRepository
{
    public IQueryable<Customer> GetAllCustomers();
    public Customer? GetCustomerById(int customerId);
    public Customer? GetCustomerByAddress(int houseNumber, string zipCode);
    public void CreateCustomer(Customer customer);
    public bool UpdateCustomer(Customer customer);
    public bool DeleteCustomer(int customerId);
}