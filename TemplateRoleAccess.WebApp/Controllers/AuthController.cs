using Microsoft.AspNetCore.Mvc;

namespace TemplateRoleAccess.WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
