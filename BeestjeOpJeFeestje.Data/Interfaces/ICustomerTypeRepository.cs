using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface ICustomerTypeRepository
{
    public IEnumerable<CustomerType> GetCustomerTypes();
    public CustomerType? GetCustomerType(int customerTypeId);
}