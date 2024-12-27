using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class CustomerType
    {
        [Key]
        [Column("customer_type_id")]
        public int Id { get; set; }

        [Column("customer_type_name")]
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
