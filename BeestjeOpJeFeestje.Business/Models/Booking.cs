namespace BeestjeOpJeFeestje.Business.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsConfirmed { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public virtual ICollection<Animal> Animals { get; set; } = [];
    }
}
