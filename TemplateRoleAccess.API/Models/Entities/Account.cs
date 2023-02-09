using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateRoleAccess.API.Models.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [Key, ForeignKey("Employee")]
        public string? NIK { get; set; }
        public string? Password { get; set; }

        // One Account have one employee  - One To One
        public virtual Employee? Employee { get; set; }

        // Many to many
        //[JsonIgnore]
        public virtual ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
