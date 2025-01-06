using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Business.Interfaces;

public interface IDiscountRule
{
    decimal CalculateDiscount(Booking booking);
}