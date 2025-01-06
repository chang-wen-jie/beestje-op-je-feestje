using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class Customer : IdentityUser
    {
        [Column("customer_name")]
        public string Name { get; set; }

        [Column("customer_house_number")]
        public int HouseNumber { get; set; }

        [Column("customer_zip_code")]
        [RegularExpression(@"^\d{4}[A-Za-z]{2}$", ErrorMessage = "Postcode moet het formaat van '1234AB' aanhouden")]
        public string ZipCode { get; set; }

        [Column("customer_type_id")]
        [Range(1, 4, ErrorMessage = "Type moet tussen de 1 en 4 liggen")]
        public int? TypeId { get; set; }
        public CustomerType? Type { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
