using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class AccountType
    {
        [Key]
        [Column("account_type_id")]
        public int Id { get; set; }

        [Column("account_type_name")]
        public string Name { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
