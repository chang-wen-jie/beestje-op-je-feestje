using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Business.Services.DiscountRules;

public class AnimalTypeDiscountService : IDiscountRule
{
    public decimal CalculateDiscount(Booking booking)
    {
        var animalGroups = booking.Animals.GroupBy(a => a.TypeId);

        return (from @group in animalGroups where @group.Count() >= 3 select 10m).Prepend(0).Max();
    }
}