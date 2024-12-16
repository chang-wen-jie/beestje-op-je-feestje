using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Business.Models
{
    public enum AnimalType
    {
        Jungle,
        Boerderij,
        Sneeuw,
        Woestijn,
        VIP,
    }

    public class Animal
    {
        [Key]
        [Column("animal_id")]
        public int Id { get; set; }

        [Column("animal_name")]
        public string Name { get; set; }

        [Column("animal_type")]
        public AnimalType Type { get; set; }

        [Column("animal_price")]
        public decimal Price { get; set; }

        [Column("animal_image_url")]
        public string ImageUrl { get; set; }

        [InverseProperty("Animals")]
        public virtual ICollection<Booking> Bookings { get; set; } = [];

        public bool IsAvailableForBooking(DateTime date)
        {
            return !Bookings.Any(booking => booking.Date == date && booking.IsConfirmed);
        }
    }
}
