using BeestjeOpJeFeestje.Data.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class BookingController(BeestjeOpJeFeestjeDbContext context) : Controller
    {
        private readonly BeestjeOpJeFeestjeDbContext _context = context;

        public IActionResult Index()
        {
            //var bookings = _context.Bookings;

            return View();
        }
    }
}
