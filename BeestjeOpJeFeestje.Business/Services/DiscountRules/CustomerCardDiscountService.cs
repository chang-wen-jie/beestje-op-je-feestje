using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Business.Services.DiscountRules;

public class CustomerCardDiscountService : IDiscountRule
{
    public decimal CalculateDiscount(Booking booking)
    {
        return booking.Customer.TypeId != null ? 10m : 0m;
    }
}