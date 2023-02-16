using Azure;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Models.ViewModels;
using TemplateRoleAccess.API.Repositories.General;

namespace TemplateRoleAccess.API.Repositories.Data
{
    public class DepartementRepository : GeneralRepository<Departement, int>
    {
        private readonly AppDbContext _appDbContext;
        public DepartementRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<GetDataManagerVM>> GetDataManager()
        {
            var response = await (from d in _appDbContext.Departements
                                  join e in _appDbContext.Employees on d.Manager_Id equals e.NIK
                                  select new GetDataManagerVM
                                  {
                                      Departement_Id = d.Id,
                                      Departement_Name = d.Name,
                                      Manager_NIK = d.Manager_Id,
                                      Manager_Name = e.FirstName + " " + e.LastName
                                  }).ToListAsync();

            return response;
        }
        
        public async Task<ObjectResult> GetDepartementName(string nik)
        {
            var response = await (from e in _appDbContext.Employees where e.NIK == nik
                                  join d in _appDbContext.Departements on e.Departement_Id equals d.Id
                                  select new
                                  {
                                      Departement_Name = d.Name,
                                  }).FirstOrDefaultAsync();

            return new ObjectResult(new
            {
                response.Departement_Name
            });
        }

        public async Task<IEnumerable<EmployeesOnDepartement>> GetMyDepartement(string nik)
        {
            var response = await (from er in _appDbContext.Employees where er.NIK == nik
                                  join e in _appDbContext.Employees on er.Departement_Id equals e.Departement_Id
                                  join d in _appDbContext.Departements on er.Departement_Id equals d.Id
                                  join ar in _appDbContext.AccountRoles on e.NIK equals ar.AccountNIK
                                  join r in _appDbContext.Roles on ar.RoleId equals r.Id
                                  select new EmployeesOnDepartement
                                  {
                                      NIK = e.NIK,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      BirthDate = e.BirthDate,
                                      Gender = e.Gender,
                                      Phone = e.Phone,
                                      Email = e.Email,
                                      Salary = e.Salary,
                                      Role_Name = r.Name
                                  }).ToListAsync();

            return response;
        }

        public async Task<ObjectResult> GetDataDepartement(string nik)
        {
            var DataManager = await (from d in _appDbContext.Departements where d.Manager_Id == nik
                                  join e in _appDbContext.Employees on d.Manager_Id equals e.NIK
                                  select new GetDataDepartementVM
                                  {
                                      Departement_Id = d.Id,
                                      Departement_Name = d.Name,
                                      Manager_NIK = d.Manager_Id,
                                      Manager_Name = e.FirstName + " " + e.LastName,
                                  }).FirstOrDefaultAsync();

            if (DataManager != null)
            {
                var DataEmployeesOfDepartement = await (from ae in _appDbContext.Employees where ae.Manager_Id == nik
                                                        join ar in _appDbContext.AccountRoles on ae.NIK equals ar.AccountNIK
                                                        join r in _appDbContext.Roles on ar.RoleId equals r.Id
                                                        select new EmployeesOnDepartement
                                                        {
                                                            NIK = ae.NIK,
                                                            FirstName = ae.FirstName,
                                                            LastName = ae.LastName,
                                                            BirthDate = ae.BirthDate,
                                                            Gender = ae.Gender,
                                                            Phone = ae.Phone,
                                                            Email = ae.Email,
                                                            Salary = ae.Salary,
                                                            Role_Name = r.Name
                                                        }).ToListAsync();

                return new ObjectResult(new
                {
                    DataManager.Departement_Id,
                    DataManager.Departement_Name,
                    DataManager.Manager_NIK,
                    DataManager.Manager_Name,
                    Employees = DataEmployeesOfDepartement
                });
            } else
            {
                return null;
            }
        }
        
        public async Task<GetDataManagerVM> GetDataManager(int id)
        {
            var response = await (from d in _appDbContext.Departements where d.Id == id
                                  join e in _appDbContext.Employees on d.Manager_Id equals e.NIK
                                  select new GetDataManagerVM
                                  {
                                      Departement_Id = d.Id,
                                      Departement_Name = d.Name,
                                      Manager_NIK = d.Manager_Id,
                                      Manager_Name = e.FirstName + " " + e.LastName
                                  }).FirstOrDefaultAsync();

            return response;
        }

        public async Task<int> DepartementUpdate(Departement departement)
        {
            Departement dpt = await _appDbContext.Departements.FindAsync(departement.Id);

            dpt.Name = departement.Name;
            dpt.Manager_Id = departement.Manager_Id;

            _appDbContext.Update(dpt);
            var response = await _appDbContext.SaveChangesAsync();

            Employee empl = await _appDbContext.Employees.FindAsync(departement.Manager_Id);
            empl.Departement_Id = departement.Id;
            empl.Manager_Id = null;

            _appDbContext.Update(empl);
            response = await _appDbContext.SaveChangesAsync();

            AccountRole acr = await _appDbContext.AccountRoles.Where(ar => ar.AccountNIK == departement.Manager_Id).FirstOrDefaultAsync();
            if(acr != null)
            {
                _appDbContext.Remove(acr);
                response = await _appDbContext.SaveChangesAsync();
            }

            List<Employee> allEmpl = await _appDbContext.Employees.Where(e => e.Departement_Id == departement.Id).ToListAsync();

            if (allEmpl != null)
            {
                AccountRole oldAR;
                AccountRole newAR;

                for (int i = 0; i < allEmpl.Count(); i++)
                {
                    allEmpl[i].Manager_Id = departement.Manager_Id;
                    
                    oldAR = await _appDbContext.AccountRoles.Where(ar => ar.AccountNIK == allEmpl[i].NIK && ar.RoleId == 2).FirstOrDefaultAsync();
                    if (oldAR != null)
                    {
                        _appDbContext.Remove(oldAR);
                        response = await _appDbContext.SaveChangesAsync();
                        
                        newAR = new AccountRole();
                        newAR.RoleId = 3;
                        newAR.AccountNIK = allEmpl[i].NIK;

                        await _appDbContext.AddAsync(newAR);
                        response = await _appDbContext.SaveChangesAsync();
                    }

                }

                await _appDbContext.BulkUpdateAsync(allEmpl);
                response = await _appDbContext.SaveChangesAsync();
            }

            var newAcr = new AccountRole();
            newAcr.RoleId = 2;
            newAcr.AccountNIK = departement.Manager_Id;

            await _appDbContext.AddAsync(newAcr);
            response = await _appDbContext.SaveChangesAsync();

            return response;
        }
        
        public async Task<int> DepartementDelete(int id)
        {
            Departement DelDpt = await _appDbContext.Departements.FindAsync(id);
            List<Employee> allEmpl = await _appDbContext.Employees.Where(e => e.Departement_Id == id).ToListAsync();
            int response;

            if (allEmpl != null)
            {
                AccountRole oldAR;

                for (int i = 0; i < allEmpl.Count(); i++)
                {
                    allEmpl[i].Departement_Id = null;
                    allEmpl[i].Manager_Id = null;

                    oldAR = await _appDbContext.AccountRoles.Where(ar => ar.AccountNIK == allEmpl[i].NIK).FirstOrDefaultAsync();
                    if (oldAR != null)
                    {
                        _appDbContext.Remove(oldAR);
                        response = await _appDbContext.SaveChangesAsync();
                    }
                }

                await _appDbContext.BulkUpdateAsync(allEmpl);
                response = await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.Remove(DelDpt);
            response = await _appDbContext.SaveChangesAsync();

            return response;
        }


    }

}
