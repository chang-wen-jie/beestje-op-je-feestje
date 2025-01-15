using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Tests.HelperClasses;

public static class BookingDiscountTestHelper
{
    public static Booking CreateBooking(List<Animal> animals, DateOnly bookingDate, Customer? customer = null)
    {
        return new Booking
        {
            Animals = animals,
            Date = bookingDate,
            CustomerId = customer?.Id ?? string.Empty,
            Customer = customer ?? new Customer(),
        };
    }

    public static Animal CreateAnimal(int typeId = 0, string name = "")
    {
        return new Animal
        {
            TypeId = typeId,
            Name = name,
        };
    }

    public static Customer CreateCustomer(string id = "1", int typeId = 0)
    {
        return new Customer
        {
            Id = id,
            TypeId = typeId,
        };
    }
}