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
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterEmployeeVM registerEmployeeVM)
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

        [HttpPut]
        [Route("Register/Update")]
        public async Task<IActionResult> RegisterUpdate(RegisterEmployeeVM registerEmployee)
        {
            var response = await _employeeRepository.RegisterUpdate(registerEmployee);
            return response switch
            {
                0 or 1 => StatusCode(201, new { Status = HttpStatusCode.Created, Message = "Data employee successfully updated", Data = response }),
                2 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, Message = "Data email is duplicate", Data = response }),
                3 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, Message = "Data phone is duplicate", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, Message = "Internal server error", Data = response })
            };
        }
        
        [HttpDelete]
        [Route("Register/Delete/{nik}")]
        public async Task<IActionResult> RegisterDelete([FromRoute] string nik)
        {
            var response = await _employeeRepository.RegisterDelete(nik);
            return response switch
            {
                0 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, Message = "Data employee failed to delete", Data = response }),
                1 => StatusCode(201, new { Status = HttpStatusCode.Created, Message = "Data employee successfully deleted", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, Message = "Internal server error", Data = response })
            };
        }

        [HttpGet]
        //[Authorize(Roles = "Admin, Manager")]
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
        
        [HttpGet]
        [Route("SpecificDataEmployees")]
        //[Authorize(Roles = "Admin, Manager")]
        public virtual async Task<IActionResult> GetSpecificEmployees()
        {
            var response = await _employeeRepository.GetSpecificEmployees();
            return response.Count() switch
            {
                0 => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = "Data not found", Data = response }),
                >= 1 => StatusCode(200, new { Status = HttpStatusCode.OK, message = $"{response.Count()} data found", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = response })
            };
        }
        
        [HttpGet]
        [Route("SpecificDataEmployees/{nik}")]
        //[Authorize(Roles = "Admin, Manager")]
        public virtual async Task<IActionResult> GetSpecificEmployees(string nik)
        {
            var response = await _employeeRepository.GetSpecificEmployees(nik);
            return response switch
            {
                null => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = "Data not found", Data = response }),
                _ => StatusCode(200, new { Status = HttpStatusCode.OK, message = $"Data with NIK {nik} found", Data = response }),
            };
        }
    }
}
