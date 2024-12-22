using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class AnimalType
    {
        [Key]
        [Column("animal_type_id")]
        public int Id { get; set; }

        [Column("animal_type_name")]
        public string Name { get; set; }

        public ICollection<Animal> Animals { get; set; }
    }
}
