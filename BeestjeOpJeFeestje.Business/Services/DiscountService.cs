using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Business.Services;

public class DiscountService(IEnumerable<IDiscountRule> discountRules)
{
    private const int MaxDiscount = 60;
    private readonly IEnumerable<IDiscountRule> _discountRules = discountRules;
    
    public decimal CalculateDiscount(Booking booking)
    {
        var discount = _discountRules.Sum(rule => rule.CalculateDiscount(booking));

        return discount > MaxDiscount ? MaxDiscount : discount;
    }
}