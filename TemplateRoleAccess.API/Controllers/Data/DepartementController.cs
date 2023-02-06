using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using TemplateRoleAccess.API.Controllers.Base;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Models.ViewModels;
using TemplateRoleAccess.API.Repositories.Data;

namespace TemplateRoleAccess.API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : BaseController<Departement, DepartementRepository, int>
    {
        private readonly DepartementRepository _departementRepository;
        public DepartementController(DepartementRepository departementRepository) : base(departementRepository)
        {
            _departementRepository = departementRepository;
        }
    }
}
