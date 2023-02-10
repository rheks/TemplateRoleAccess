using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateRoleAccess.API.Models.Entities
{
    [Table("AccountRoles")]
    public class AccountRole
    {
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual Role Roles { get; set; }

        [ForeignKey("Accounts")]
        public string? AccountNIK { get; set; }
        [JsonIgnore]
        public virtual Account Accounts { get; set; }
    }

}
