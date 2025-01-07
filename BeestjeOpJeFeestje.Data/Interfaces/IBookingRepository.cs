using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface IBookingRepository
{
    public IEnumerable<Booking> GetAllBookings();
    public Booking? GetBookingById(int id);
    public IQueryable<Booking> GetBookingsByCustomerId(string customerId);
    public void AddBooking(Booking booking);
    public bool DeleteBooking(int id);
}