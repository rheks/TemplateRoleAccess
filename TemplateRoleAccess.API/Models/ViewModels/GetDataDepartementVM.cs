using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TemplateRoleAccess.API.Models.Entities;

namespace TemplateRoleAccess.API.Models.ViewModels
{
    public class GetDataDepartementVM
    {
        public int Departement_Id { get; set; }
        public string? Departement_Name { get; set; }
        public string? Manager_NIK { get; set; }
        public string? Manager_Name { get; set; }
    }

    public class EmployeesOnDepartement
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
        public string Role_Name { get; set; }
    }
}
