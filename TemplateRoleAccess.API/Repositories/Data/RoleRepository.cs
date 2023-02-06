using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Repositories.General;

namespace TemplateRoleAccess.API.Repositories.Data
{
    public class RoleRepository : GeneralRepository<Role, int>
    {
        private readonly AppDbContext _appDbContext;
        public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
