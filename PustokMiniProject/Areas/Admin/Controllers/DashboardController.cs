﻿using Microsoft.AspNetCore.Mvc;

namespace PustokMiniProject.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
