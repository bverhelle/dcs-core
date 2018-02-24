using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DCSCoreMvc.Controllers
{
    public class HairdreamsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Long()
        {
            return View();
        }

        public IActionResult Volume()
        {
            return View();
        }
    }
}