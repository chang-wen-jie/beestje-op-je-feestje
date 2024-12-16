using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Business.Models
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

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [InverseProperty("Bookings")]
        public virtual ICollection<Animal> Animals { get; set; } = [];

        public bool ConfirmBooking()
        {
            foreach (var animal in Animals)
            {
                if (!animal.IsAvailableForBooking(Date))
                {
                    return false;
                }
            }

            IsConfirmed = true;
            TotalPrice = Animals.Sum(a => a.Price);

            foreach (var animal in Animals) animal.Bookings.Add(this);

            return true;
        }
    }
}
