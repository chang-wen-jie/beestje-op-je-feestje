namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingFormStateViewModel
    {
        public string Date { get; set; }
        public List<BeestjeOpJeFeestje.Data.Models.Animal>? Animals { get; set; }
        public BeestjeOpJeFeestje.Data.Models.Customer? Customer { get; set; }
    }
}
