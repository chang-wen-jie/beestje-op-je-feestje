using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingFormViewModel
    {
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscountPercentage { get; set; }
        public BookingFormStateViewModel BookingFormState { get; set; }
    }
}
