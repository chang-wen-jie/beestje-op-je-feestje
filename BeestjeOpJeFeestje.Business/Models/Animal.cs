namespace BeestjeOpJeFeestje.Business.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = [];

        public bool isAvailable(DateTime date)
        {
            return !Bookings.Any(booking => booking.Date == date && booking.IsConfirmed);
        }
    }
}
