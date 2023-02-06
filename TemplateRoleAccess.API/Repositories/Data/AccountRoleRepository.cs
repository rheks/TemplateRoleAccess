using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Repositories.General;

namespace TemplateRoleAccess.API.Repositories.Data
{
    public class AccountRoleRepository : GeneralRepository<AccountRole, int>
    {
        private readonly AppDbContext _appDbContext;
        public AccountRoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
