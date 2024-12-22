using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class Booking
    {
        [Key]
        [Column("booking_id")]
        public int Id { get; set; }

        [Column("booking_date")]
        public DateTime Date { get; set; }

        [Column("booking_total_price")]
        public decimal TotalPrice { get; set; }

        [Column("booking_is_confirmed")]
        public bool IsConfirmed { get; set; }

        [Column("booking_account_id")]
        public int AccountId { get; set; }

        public Account Account { get; set; }

        [InverseProperty("Bookings")]
        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
