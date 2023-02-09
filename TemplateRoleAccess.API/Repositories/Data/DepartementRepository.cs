using Microsoft.EntityFrameworkCore;
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
    }

    //public async Task<int> DepartementAdd(Departement departement)
    //{
        

    //    return 0;
    //}
}
