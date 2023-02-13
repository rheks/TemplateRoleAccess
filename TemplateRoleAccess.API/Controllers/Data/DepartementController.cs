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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Get()
        {
            var response = await _departementRepository.Get();
            return response.Count() switch
            {
                0 => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = "Data not found", Data = response }),
                >= 1 => StatusCode(200, new { Status = HttpStatusCode.OK, message = $"{response.Count()} data found", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = response })
            };
        }

        [HttpGet]
        [Route("GetDataManager")]
        //[Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetDataManager()
        {
            var response = await _departementRepository.GetDataManager();
            return response.Count() switch
            {
                0 => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = "Data not found", Data = response }),
                >= 1 => StatusCode(200, new { Status = HttpStatusCode.OK, message = $"{response.Count()} data found", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = response }),
            };
        }
        
        [HttpGet]
        [Route("GetDataManager/{id}")]
        //[Authorize(Roles = "Admin, Manager")]
        public virtual async Task<IActionResult> GetDataManager(int id)
        {
            var response = await _departementRepository.GetDataManager(id);
            return response switch
            {
                null => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = "Data not found", Data = response }),
                _ => StatusCode(200, new { Status = HttpStatusCode.OK, message = $"Data with Id {id} found", Data = response }),
            };
        }

        [HttpPut]
        [Route("UpdateSpecificDepartement")]
        public async Task<IActionResult> DepartementUpdate(Departement departement)
        {
            var response = await _departementRepository.DepartementUpdate(departement);
            return response switch
            {
                0 or 1 => StatusCode(201, new { Status = HttpStatusCode.Created, Message = "Data departement successfully updated", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, Message = "Internal server error", Data = response })
            };
        }
        
        [HttpDelete]
        [Route("DeleteSpecificDepartement/{id}")]
        public async Task<IActionResult> DepartementDelete([FromRoute] int id)
        {
            var response = await _departementRepository.DepartementDelete(id);
            return response switch
            {
                0 or 1 => StatusCode(201, new { Status = HttpStatusCode.Created, Message = "Data departement successfully deleted", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, Message = "Internal server error", Data = response })
            };
        }
    }
}
