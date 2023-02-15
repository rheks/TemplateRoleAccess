using BC = BCrypt.Net.BCrypt;
using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Repositories.General;
using TemplateRoleAccess.API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using EFCore.BulkExtensions;

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

            actsRole.RoleId = registerEmployee.Role_Id;
            actsRole.AccountNIK = newNIK;
            await _appDbContext.AddAsync(actsRole);
            response = await _appDbContext.SaveChangesAsync();

            return response;
        }
        
        public async Task<int> RegisterUpdate(RegisterEmployeeVM registerEmployee)
        {
            var checkEmail = await _appDbContext.Employees.Where(e => e.Email == registerEmployee.Email).Take(2).ToListAsync();
            var checkPhone = await _appDbContext.Employees.Where(e => e.Phone == registerEmployee.Phone).Take(2).ToListAsync();

            if (checkEmail.Count() > 1)
            {
                return 2;
            }
            else if (checkPhone.Count() > 1)
            {
                return 3;
            }

            Employee empl = await _appDbContext.Employees.FindAsync(registerEmployee.NIK);
            empl.NIK = registerEmployee.NIK;
            empl.FirstName = registerEmployee.FirstName;
            empl.LastName = registerEmployee.LastName;
            empl.BirthDate = registerEmployee.BirthDate;
            empl.Gender = registerEmployee.Gender;
            empl.Phone = registerEmployee.Phone;
            empl.Email = registerEmployee.Email;
            empl.Salary = registerEmployee.Salary;
            empl.Departement_Id = registerEmployee.Departement_Id == 0 ? null : registerEmployee.Departement_Id;
            empl.Manager_Id = registerEmployee.Manager_Id;
            
            _appDbContext.Update(empl);
            var response = await _appDbContext.SaveChangesAsync();

            AccountRole actsRole = await _appDbContext.AccountRoles.SingleOrDefaultAsync(ar => ar.AccountNIK == registerEmployee.NIK);

            if (actsRole != null)
            {
                _appDbContext.Remove(actsRole);
                response = await _appDbContext.SaveChangesAsync();
            }

            var newAcr = new AccountRole();
            newAcr.RoleId = registerEmployee.Role_Id;
            newAcr.AccountNIK = registerEmployee.NIK;

            await _appDbContext.AddAsync(newAcr);
            response = await _appDbContext.SaveChangesAsync();

            Departement emplDpt = await _appDbContext.Departements.Where(d => d.Manager_Id == empl.NIK).SingleOrDefaultAsync();

            if (emplDpt != null)
            {
                emplDpt.Manager_Id = null;

                _appDbContext.Update(emplDpt);
                response = await _appDbContext.SaveChangesAsync();
            }

            List<Employee> allEmpl = await _appDbContext.Employees.Where(e => e.Manager_Id == registerEmployee.NIK).ToListAsync();
            
            if (allEmpl != null)
            {
                for (int i = 0; i < allEmpl.Count(); i++)
                {
                    allEmpl[i].Manager_Id = null;
                }

                await _appDbContext.BulkUpdateAsync(allEmpl);
            }

            return response;
        }
        
        public async Task<int> RegisterDelete(string nik)
        {
            Employee empl = await _appDbContext.Employees.FindAsync(nik);
            List<Employee> allEmpl = await _appDbContext.Employees.Where(e => e.Manager_Id == nik).ToListAsync();

            if (allEmpl != null)
            {
                for (int i = 0; i < allEmpl.Count(); i++)
                {
                    allEmpl[i].Manager_Id = null;
                }
            }
            
            await _appDbContext.BulkUpdateAsync(allEmpl);

            AccountRole actsRole = await _appDbContext.AccountRoles.SingleOrDefaultAsync(ar => ar.AccountNIK == nik);
            Departement dep = await _appDbContext.Departements.SingleOrDefaultAsync(ar => ar.Manager_Id == nik);
            int response;

            if (dep != null)
            {
                dep.Manager_Id = null;
                _appDbContext.Update(dep);
                response = await _appDbContext.SaveChangesAsync();
            }

            if (actsRole != null)
            {
                _appDbContext.Remove(actsRole);
                response = await _appDbContext.SaveChangesAsync();
            }
            
            _appDbContext.Remove(empl);
            response = await _appDbContext.SaveChangesAsync();

            return response;
        }
        
        public async Task<IEnumerable<GetSpecificEmployeesVM>> GetSpecificEmployees()
        {
            var response = await (from e in _appDbContext.Employees
                                  join d in _appDbContext.Departements on e.Departement_Id equals d.Id into ed
                                  from newEd in ed.DefaultIfEmpty()
                                  join ar in _appDbContext.AccountRoles on e.NIK equals ar.AccountNIK into ArE
                                  from newArE in ArE.DefaultIfEmpty()
                                  join r in _appDbContext.Roles on newArE.RoleId equals r.Id into edarr
                                  from subedarr in edarr.DefaultIfEmpty()

                                  select new GetSpecificEmployeesVM
                                  {
                                      NIK = e.NIK,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      BirthDate = e.BirthDate,
                                      Gender = e.Gender,
                                      Phone = e.Phone,
                                      Email = e.Email,
                                      Salary = e.Salary,
                                      Role_Id = subedarr.Id,
                                      Role_Name = subedarr.Name,
                                      Departement_Id = newEd.Id,
                                      Departement_Name = newEd.Name,
                                      Manager_Id = e.Manager_Id
                                  }).ToListAsync();

            return response;
        }
        
        public async Task<GetSpecificEmployeesVM> GetSpecificEmployees(string nik)
        {
            var response = await (from e in _appDbContext.Employees where e.NIK == nik
                                  join d in _appDbContext.Departements on e.Departement_Id equals d.Id into ed
                                  from newEd in ed.DefaultIfEmpty()
                                  join ar in _appDbContext.AccountRoles on e.NIK equals ar.AccountNIK into ArE
                                  from newArE in ArE.DefaultIfEmpty()
                                  join r in _appDbContext.Roles on newArE.RoleId equals r.Id into edarr
                                  from subedarr in edarr.DefaultIfEmpty()

                                  select new GetSpecificEmployeesVM
                                  {
                                      NIK = e.NIK,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      BirthDate = e.BirthDate,
                                      Gender = e.Gender,
                                      Phone = e.Phone,
                                      Email = e.Email,
                                      Salary = e.Salary,
                                      Role_Id = subedarr.Id,
                                      Role_Name = subedarr.Name,
                                      Departement_Id = newEd.Id,
                                      Departement_Name = newEd.Name,
                                      Manager_Id = e.Manager_Id
                                  }).FirstOrDefaultAsync();

            return response;
        }
    }
}
