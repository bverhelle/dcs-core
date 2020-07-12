using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSCoreMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCSCoreMvc.Controllers
{
  public class BabyListController : Controller
  {
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(BabyListViewModel model)
    {
      if (ModelState.IsValid)
      {

        return RedirectToAction(nameof(Enlist));
      }

      return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Enlist()
    {
      return View();
    }



  }
}