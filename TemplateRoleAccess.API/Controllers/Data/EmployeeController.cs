using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TemplateRoleAccess.API.Controllers.Base;
using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Models.ViewModels;
using TemplateRoleAccess.API.Repositories.Data;

namespace TemplateRoleAccess.API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin")]
        public virtual async Task<IActionResult> Register(RegisterEmployeeVM registerEmployeeVM)
        {
            var response = await _employeeRepository.Register(registerEmployeeVM);
            return response switch
            {
                0 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, message = "Employee failed to register", Data = new List<Object> { } }),
                1 => StatusCode(200, new { Status = HttpStatusCode.OK, message = "Employee successfully registered", Data = new List<Object> { } }),
                2 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, message = "Email already registered", Data = new List<Object> { } }),
                3 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, message = "Phone already registered", Data = new List<Object> { } }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = new List<Object> { } })
            };
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public override async Task<IActionResult> Get()
        {
            var response = await _employeeRepository.Get();
            return response.Count() switch
            {
                0 => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = "Data not found", Data = response }),
                >= 1 => StatusCode(200, new { Status = HttpStatusCode.OK, message = $"{response.Count()} data found", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = response })
            };
        }
    }
}
