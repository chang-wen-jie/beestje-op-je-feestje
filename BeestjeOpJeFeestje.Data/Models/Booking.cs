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
        public DateOnly Date { get; set; }
        
        [Column("booking_customer_id")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("booking_total_price")]
        public decimal TotalPrice { get; set; }
        
        [Column("booking_total_discount_percentage")]
        public decimal TotalDiscountPercentage { get; set; }

        [InverseProperty("Bookings")]
        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
