using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int Id { get; set; }

        [Column("customer_password")]
        public string Password { get; set; }

        [Column("customer_name")]
        public string Name { get; set; }

        [Column("customer_house_number")]
        public int HouseNumber { get; set; }

        [Column("customer_zip_code")]
        [RegularExpression(@"^\d{4}[A-Za-z]{2}$", ErrorMessage = "Postcode moet het formaat van '1234AB' aanhouden")]
        public string ZipCode { get; set; }

        [Column("customer_email_address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "E-mailadres moet een geldige formaat aanhouden")]
        public string? EmailAddress { get; set; }

        [Column("customer_phone_number")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Telefoonnummer moet het internationale of binnenlandse formaat aanhouden")]
        public string? PhoneNumber { get; set; }

        [Column("customer_type_id")]
        [Range(1, 4, ErrorMessage = "Type moet tussen de 1 en 4 liggen")]
        public int? TypeId { get; set; }
        
        public CustomerType Type { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
