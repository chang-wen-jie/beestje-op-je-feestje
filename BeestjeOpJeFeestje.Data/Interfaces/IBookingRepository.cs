using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface IBookingRepository
{
    public IQueryable<Booking> GetAllBookings();
    public Booking? GetBookingById(int id);
    public IQueryable<Booking> GetBookingsByCustomerId(string customerId);
    public void CreateBooking(Booking booking);
    public bool DeleteBooking(int id);
}