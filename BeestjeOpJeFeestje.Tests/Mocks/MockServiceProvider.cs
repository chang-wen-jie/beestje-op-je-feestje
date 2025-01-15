using BeestjeOpJeFeestje.Data.Interfaces;

namespace BeestjeOpJeFeestje.Tests.Mocks
{
    public class MockServiceProvider(IAnimalRepository animalRepository, ICustomerRepository customerRepository)
        : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IAnimalRepository)) return animalRepository;
            if (serviceType == typeof(ICustomerRepository)) return customerRepository;
            throw new InvalidOperationException();
        }
    }
}