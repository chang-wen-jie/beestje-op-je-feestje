using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Business.Services.DiscountRules;

public class NameLetterDiscountService : IDiscountRule
{
    public decimal CalculateDiscount(Booking booking)
    {
        return "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            .Where(letter => booking.Animals
                .Any(a => a.Name.IndexOf(letter.ToString(), StringComparison.CurrentCultureIgnoreCase) >= 0))
            .Distinct()
            .Sum(letter => 2m);
    }
}