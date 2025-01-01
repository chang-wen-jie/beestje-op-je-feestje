using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class Animal
    {
        [Key]
        [Column("animal_id")]
        public int Id { get; set; }

        [Column("animal_name")]
        public string Name { get; set; }

        [Column("animal_type_id")]
        [Range(1, 5, ErrorMessage = "Type moet tussen de 1 en 5 liggen")]
        public int TypeId { get; set; }
        public AnimalType Type { get; set; }

        [Column("animal_price")]
        public decimal Price { get; set; }

        [Column("animal_image_url")]
        public string ImageUrl { get; set; }

        [InverseProperty("Animals")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
