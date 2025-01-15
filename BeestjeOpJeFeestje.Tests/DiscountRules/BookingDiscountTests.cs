using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Business.Services.DiscountRules;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Tests.HelperClasses;

namespace BeestjeOpJeFeestje.Tests.DiscountRules
{
    public class DiscountServiceTests
    {
        private IDiscountRule? _discountRule;

        [Test]
        public void AnimalTypeDiscountService_WhenBookingHas3SameTypeAnimals_ShouldApply10PercentDiscount()
        {
            _discountRule = new AnimalTypeDiscountService();
            var animals = new List<Animal>
            {
                BookingDiscountTestHelper.CreateAnimal(typeId: 1),
                BookingDiscountTestHelper.CreateAnimal(typeId: 1),
                BookingDiscountTestHelper.CreateAnimal(typeId: 1)
            };
            var booking = BookingDiscountTestHelper.CreateBooking(animals, new DateOnly(2025, 1, 1));
            var discount = _discountRule.CalculateDiscount(booking);

            Assert.That(discount, Is.EqualTo(10m));
        }

        [Test]
        public void CustomerCardDiscountService_WhenCustomerHasCard_ShouldApply10PercentDiscount()
        {
            _discountRule = new CustomerCardDiscountService();
            var animals = new List<Animal> { BookingDiscountTestHelper.CreateAnimal(name: "Zebra") };
            var customer = BookingDiscountTestHelper.CreateCustomer(typeId: 1);
            var booking = BookingDiscountTestHelper.CreateBooking(animals, new DateOnly(2025, 1, 1), customer);
            var discount = _discountRule.CalculateDiscount(booking);

            Assert.That(discount, Is.EqualTo(10m));
        }

        [Test]
        public void DayOfWeekDiscountService_WhenBookingOnMonday_ShouldApply15PercentDiscount()
        {
            _discountRule = new DayOfWeekDiscountService();
            var animals = new List<Animal> { BookingDiscountTestHelper.CreateAnimal(name: "Aap") };
            var booking = BookingDiscountTestHelper.CreateBooking(animals, new DateOnly(2025, 1, 13));
            var discount = _discountRule.CalculateDiscount(booking);

            Assert.That(discount, Is.EqualTo(15m));
        }

        [Test]
        public void DuckDiscountService_WhenBookingContainsDuck_ShouldApply50PercentDiscountWithChance()
        {
            _discountRule = new DuckDiscountService();
            var animals = new List<Animal> { BookingDiscountTestHelper.CreateAnimal(name: "Eend") };
            var booking = BookingDiscountTestHelper.CreateBooking(animals, new DateOnly(2025, 1, 1));
            var discount = _discountRule.CalculateDiscount(booking);

            Assert.That(discount is 50m or 0m);
        }

        [Test]
        public void NameLetterDiscountService_WhenAnimalNamesContainsDuplicateLetters_ShouldNotApply2PercentDiscount()
        {
            _discountRule = new NameLetterDiscountService();
            var animals = new List<Animal>
            {
                BookingDiscountTestHelper.CreateAnimal(name: "Hond"),
                BookingDiscountTestHelper.CreateAnimal(name: "Zeehond")
            };
            var booking = BookingDiscountTestHelper.CreateBooking(animals, new DateOnly(2025, 1, 1));
            var discount = _discountRule.CalculateDiscount(booking);

            Assert.That(discount, Is.EqualTo(12m));
        }
    }
}
