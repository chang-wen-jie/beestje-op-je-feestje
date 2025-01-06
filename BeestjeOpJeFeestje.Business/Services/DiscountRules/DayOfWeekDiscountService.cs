using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Business.Services.DiscountRules;

public class DayOfWeekDiscountService : IDiscountRule
{
    public decimal CalculateDiscount(Booking booking)
    {
        return booking.Date.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Tuesday ? 15m : 0m;
    }
}