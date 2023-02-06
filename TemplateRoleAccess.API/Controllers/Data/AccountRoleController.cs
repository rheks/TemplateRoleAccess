using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TemplateRoleAccess.API.Controllers.Base;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Repositories.Data;

namespace TemplateRoleAccess.API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository _accountRoleRepository;
        public AccountRoleController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }
    }
}
