using BeestjeOpJeFeestje.Web.ViewModels.Animal;
using BeestjeOpJeFeestje.Web.ViewModels.Customer;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscountPercentage { get; set; }
        public ICollection<AnimalViewModel> Animals { get; set; } 
    }
}
