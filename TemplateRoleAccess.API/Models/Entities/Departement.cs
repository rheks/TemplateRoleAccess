using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TemplateRoleAccess.API.Models.Entities
{
    [Table("Departements")]
    public class Departement
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // One departement have one manager - One To One
        //[JsonIgnore]
        public virtual Employee ManagerDepartement { get; set; }
        [ForeignKey("ManagerDepartement")]
        public string? Manager_Id { get; set; }
    }
}
