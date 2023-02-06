using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TemplateRoleAccess.API.Controllers.Base;
using TemplateRoleAccess.API.Models.Entities;
using TemplateRoleAccess.API.Models.ViewModels;
using TemplateRoleAccess.API.Repositories.Data;

namespace TemplateRoleAccess.API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository _accountRepository;
        public AccountController(AccountRepository accountRepository) : base(accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public virtual async Task<IActionResult> Login(LoginVM userLogin)
        {
            var response = await _accountRepository.Login(userLogin);
            return response switch
            {
                null => StatusCode(404, new { Status = HttpStatusCode.NotFound, message = "NIK or password is invalid", Data = new List<Object> { } }),
                _ => StatusCode(200, new { Status = HttpStatusCode.OK, message = "Login succeeded", Data = response }),
            };
        }
    }
}
