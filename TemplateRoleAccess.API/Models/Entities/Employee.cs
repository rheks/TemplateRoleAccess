using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TemplateRoleAccess.API.Models.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public string? NIK { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? Phone { get; set; }

        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }

        // Self Reference of Table Employee - One To Many
        [JsonIgnore]
        public virtual Employee ManagerEmployees { get; set; }
        [ForeignKey("ManagerEmployees")]
        public string? Manager_Id { get; set; }

        // Many employees have one departement - Many To One
        //[JsonIgnore]
        public virtual Departement Departements { get; set; }
        [ForeignKey("Departements")]
        public int? Departement_Id { get; set; }

        // One employee have one account  - One To One
        //[JsonIgnore]
        public virtual Account Account { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }
}
