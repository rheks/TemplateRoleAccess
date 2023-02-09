using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TemplateRoleAccess.API.Models.Entities;

namespace TemplateRoleAccess.API.Models.ViewModels
{
    public class GetSpecificEmployeesVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public int Role_Id { get; set; }
        public string Role_Name { get; set; }
        public int Departement_Id { get; set; }
        public string Departement_Name { get; set; }
        public string Manager_Id { get; set; }
    }
}
