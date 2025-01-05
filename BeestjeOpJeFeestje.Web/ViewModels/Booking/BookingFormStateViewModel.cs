using BeestjeOpJeFeestje.Web.ViewModels.Animal;
using BeestjeOpJeFeestje.Web.ViewModels.Customer;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingFormStateViewModel
    {
        public string? Date { get; init; }
        public List<AnimalViewModel>? Animals { get; init; }
        public CustomerViewModel? Customer { get; set; }
    }
}
