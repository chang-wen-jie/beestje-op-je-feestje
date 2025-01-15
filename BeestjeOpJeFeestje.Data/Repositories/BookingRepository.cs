using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class BookingRepository(BeestjeOpJeFeestjeDbContext context) : IBookingRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IQueryable<Booking> GetAllBookings()
    {
        return _context.Bookings.Include(b => b.Animals)
            .Include(b => b.Customer);
    }
    
    public Booking? GetBookingById(int id)
    {
        var bookingToRead = _context.Bookings.Include(b => b.Customer)
            .Include(b => b.Animals)
            .FirstOrDefault(b => b.Id == id);
        return bookingToRead;
    }

    public IQueryable<Booking> GetBookingsByCustomerId(string customerId)
    {
        return _context.Bookings.Include(b => b.Animals)
            .Where(b => b.CustomerId == customerId);
    }

    public void CreateBooking(Booking booking)
    {
        _context.Bookings.Add(booking);
        _context.SaveChanges();
    }

    public bool DeleteBooking(int id)
    {
        var bookingToDelete = _context.Bookings.Find(id);
        if (bookingToDelete == null) return false;
        
        _context.Bookings.Remove(bookingToDelete);
        _context.SaveChanges();
        return true;
    }
}