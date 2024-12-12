using BeestjeOpJeFeestje.Data.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Web.Controllers
{
    public class BookingController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Index()
        {
            //var bookings = _context.Bookings;

            return View();
        }
    }
}
