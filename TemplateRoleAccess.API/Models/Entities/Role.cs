using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TemplateRoleAccess.API.Models.Entities
{
    [Table("Roles")]
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Many to many
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
