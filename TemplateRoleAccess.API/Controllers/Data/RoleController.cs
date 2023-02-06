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
    public class RoleController : BaseController<Role, RoleRepository, int>
    {
        private readonly RoleRepository _roleRepository;
        public RoleController(RoleRepository roleRepository) : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }
    }
}
