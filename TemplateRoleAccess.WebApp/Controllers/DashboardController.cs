﻿using Microsoft.AspNetCore.Mvc;

namespace TemplateRoleAccess.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View();
        }

        public IActionResult Departements()
        {
            return View();
        }
    }
}
