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
        
        [Column("booking_customer_id")]
        public int? CustomerId { get; set; }

        [Column("booking_total_price")]
        public decimal TotalPrice { get; set; }
        
        [Column("booking_discount_amount")]
        public decimal DiscountAmount { get; set; }

        public Customer Customer { get; set; }

        [InverseProperty("Bookings")]
        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
