using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TemplateRoleAccess.API.Repositories.Interfaces;

namespace TemplateRoleAccess.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IGeneralRepository<Entity, Key>
    {
        private readonly Repository _repository;
        public BaseController(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var response = await _repository.Get();
            return response.Count() switch
            {
                0 => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = "Data not found", Data = response }),
                >= 1 => StatusCode(200, new { Status = HttpStatusCode.OK, message = $"{response.Count()} data found", Data = response }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = response })
            };
        }

        [HttpGet]
        [Route("{Key}")]
        public virtual async Task<IActionResult> Get([FromRoute] Key key)
        {
            var response = await _repository.Get(key);
            return response switch
            {
                null => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = $"Data with id {key} not found", Data = response }),
                _ => StatusCode(200, new { Status = HttpStatusCode.OK, message = $"Data with id {key} found", Data = response })
            };
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(Entity entity)
        {
            var response = await _repository.Post(entity);
            return response switch
            {
                0 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, message = "Data failed to create", Data = new List<Object> { } }),
                1 => StatusCode(201, new { Status = HttpStatusCode.Created, message = "Data successfully created", Data = new List<Object> { } }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = new List<Object> { } })
            };
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(Entity entity)
        {
            var response = await _repository.Update(entity);
            return response switch
            {
                0 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, message = "Data failed to update", Data = new List<Object> { } }),
                1 => StatusCode(200, new { Status = HttpStatusCode.OK, message = "Data successfully updated", Data = new List<Object> { } }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = new List<Object> { } })
            };
        }

        [HttpDelete]
        [Route("{Key}")]
        public virtual async Task<IActionResult> Delete([FromRoute] Key key)
        {
            var response = await _repository.Delete(key);
            return response switch
            {
                0 => StatusCode(400, new { Status = HttpStatusCode.BadRequest, message = "Data failed to delete", Data = new List<Object> { } }),
                1 => StatusCode(200, new { Status = HttpStatusCode.OK, message = "Data successfully deleted", Data = new List<Object> { } }),
                _ => StatusCode(500, new { Status = HttpStatusCode.InternalServerError, message = "Internal server error", Data = new List<Object> { } })
            };
        }
    }
}
