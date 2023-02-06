using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Models.Entities;
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
    }
}
