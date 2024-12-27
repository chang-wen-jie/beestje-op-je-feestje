using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class BookingRepository(BeestjeOpJeFeestjeDbContext context) : IBookingRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IEnumerable<Booking> GetBookings()
    {
        return _context.Bookings;
    }
    
    public Booking? GetBooking(int bookingId)
    {
        var bookingToRead = _context.Bookings.Find(bookingId);
        return bookingToRead;
    }

    public void AddBooking(Booking booking)
    {
        _context.Bookings.Add(booking);
        _context.SaveChanges();
    }

    public bool UpdateBooking(Booking booking)
    {
        var bookingToUpdate = _context.Bookings.Find(booking.Id);
        if (bookingToUpdate == null) return false;
        
        _context.Entry(bookingToUpdate).CurrentValues.SetValues(booking);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteBooking(int bookingId)
    {
        var bookingToDelete = _context.Bookings.Find(bookingId);
        if (bookingToDelete == null) return false;
        
        _context.Bookings.Remove(bookingToDelete);
        _context.SaveChanges();
        return true;
    }
}