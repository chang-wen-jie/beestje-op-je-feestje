using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Business.Services.DiscountRules;

public class DuckDiscountService : IDiscountRule
{
    private readonly Random _random = new();
    
    public decimal CalculateDiscount(Booking booking)
    {
        var animal = booking.Animals.FirstOrDefault(a => a.Name == "Eend");

        if (animal != null)
        {
            return _random.Next(1, 7) == 1 ? 50m : 0m;
        }
        
        return 0m;
    }
}