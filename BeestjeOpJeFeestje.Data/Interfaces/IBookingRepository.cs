using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface IBookingRepository
{
    public IEnumerable<Booking> GetBookings();
    public Booking? GetBooking(int bookingId);
    public void AddBooking(Booking booking);
    public bool UpdateBooking(Booking booking);
    public bool DeleteBooking(int bookingId);
}