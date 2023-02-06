using BC = BCrypt.Net.BCrypt;
using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Repositories.General;
using TemplateRoleAccess.API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TemplateRoleAccess.API.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        public string GenerateNIK()
        {
            var lastNIK = "";
            var newNIK = _appDbContext.Employees.ToList().Count + 1;

            if (newNIK >= 1 && newNIK <= 9)
            {
                lastNIK = "000" + Convert.ToString(newNIK);
            }
            else if (newNIK >= 10 && newNIK <= 99)
            {
                lastNIK = "00" + Convert.ToString(newNIK);
            }
            else if (newNIK >= 100 && newNIK <= 999)
            {
                lastNIK = "0" + Convert.ToString(newNIK);
            }

            DateTime dateTime = DateTime.UtcNow.Date;
            lastNIK = dateTime.ToString("yyyyddMM") + lastNIK;
            return lastNIK;
        }

        public async Task<int> Register(RegisterEmployeeVM registerEmployee)
        {
            if (await _appDbContext.Employees.SingleOrDefaultAsync(e => e.Email == registerEmployee.Email) != null)
            {
                return 2;
            }
            else if (await _appDbContext.Employees.SingleOrDefaultAsync(e => e.Phone == registerEmployee.Phone) != null)
            {
                return 3;
            }

            var newNIK = GenerateNIK();
            var empl = new Employee();

            empl.NIK = newNIK;
            empl.FirstName = registerEmployee.FirstName;
            empl.LastName = registerEmployee.LastName;
            empl.BirthDate = registerEmployee.BirthDate;
            empl.Gender = registerEmployee.Gender;
            empl.Phone = registerEmployee.Phone;
            empl.Email = registerEmployee.Email;
            empl.Salary = registerEmployee.Salary;
            empl.Departement_Id = registerEmployee.Departement_Id == 0 ? null : registerEmployee.Departement_Id;
            empl.Manager_Id = registerEmployee.Manager_Id;
            await _appDbContext.AddAsync(empl);
            var response = await _appDbContext.SaveChangesAsync();

            //var generatePassword = RandomString(10);
            //var HashPassword = BC.HashPassword(generatePassword);

            var acts = new Account();
            acts.NIK = newNIK;
            acts.Password = BC.HashPassword(registerEmployee.Password);
            await _appDbContext.AddAsync(acts);
            response = await _appDbContext.SaveChangesAsync();

            var actsRole = new AccountRole();
            //actsRole.RoleId = registerEmployee.Role_Id;
            
            actsRole.RoleId = 3;
            actsRole.AccountNIK = newNIK;
            await _appDbContext.AddAsync(actsRole);
            response = await _appDbContext.SaveChangesAsync();

            return response;
        }
    }
}
