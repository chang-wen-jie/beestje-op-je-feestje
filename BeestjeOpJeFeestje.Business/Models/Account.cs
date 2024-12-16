using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Business.Models
{
    public class Account
    {
        [Key]
        [Column("account_id")]
        public int Id { get; set; }

        [Column("account_name")]
        public string Name { get; set; }

        [Column("account_house_number")]
        public int HouseNumber { get; set; }

        [Column("account_zip_code")]
        [RegularExpression(@"^\d{4}[A-Za-z]{2}$", ErrorMessage = "Postcode moet het formaat van '1234AB' aanhouden.")]
        public string ZipCode { get; set; }

        [Column("account_email_address")]
        public string EmailAddress { get; set; }

        [Column("account_phone_number")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Telefoonnummer moet het internationale of binnenlandse formaat aanhouden.")]
        public string PhoneNumber { get; set; }

        [InverseProperty("Account")]
        public virtual ICollection<Booking> Bookings { get; set; } = [];

    }
}
