using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingFormViewModel
    {
        [Required(ErrorMessage = "Datum is verplicht")]
        public DateTime Date { get; set; }
        public BeestjeOpJeFeestje.Data.Models.Customer Customer { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
