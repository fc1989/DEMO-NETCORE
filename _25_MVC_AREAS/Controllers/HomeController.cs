﻿using Microsoft.AspNetCore.Mvc;

namespace _25_MVC_AREAS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
