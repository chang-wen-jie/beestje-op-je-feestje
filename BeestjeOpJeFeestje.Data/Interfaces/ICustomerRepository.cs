using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface ICustomerRepository
{
    public IEnumerable<Customer> GetAllCustomers();
    public Customer? GetCustomerById(int customerId);
    public void AddCustomer(Customer customer);
    public bool UpdateCustomer(Customer customer);
    public bool DeleteCustomer(int customerId);
}