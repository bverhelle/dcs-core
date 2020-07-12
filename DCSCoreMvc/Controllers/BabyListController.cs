using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSCoreMvc.Data;
using DCSCoreMvc.Data.Models;
using DCSCoreMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DCSCoreMvc.Controllers
{
  public class BabyListController : BaseController
  {

    public BabyListController(
      ApplicationDbContext context,
      RoleManager<IdentityRole> roleManager,
      UserManager<ApplicationUser> userManager,
      IConfiguration configuration) : base(context, roleManager, userManager, configuration)
    {
    }

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
        var alreadyEnlisted = dbContext.Set<BabyListEntry>().Where(e => e.Email == model.Email).Count() > 0;
        if (!alreadyEnlisted)
        {
          dbContext.Set<BabyListEntry>().Add(new BabyListEntry() { Email = model.Email, CreatedDate = DateTimeOffset.Now });
          await dbContext.SaveChangesAsync();
          TempData["Email"] = model.Email;
          return RedirectToAction(nameof(Enlisted));
        }
        if (alreadyEnlisted)
        {
          return RedirectToAction(nameof(AlreadyEnlisted));
        }

      }

      return View(model);
    }

    [HttpGet("Enlisted")]
    [AllowAnonymous]
    public IActionResult Enlisted()
    {
      ViewData["Email"] = TempData["Email"];

      return View();
    }

    [HttpGet("AlreadyEnlisted")]
    [AllowAnonymous]
    public IActionResult AlreadyEnlisted()
    {
      return View();
    }

    [HttpGet("AdminList")]
    [AllowAnonymous]
    public IActionResult AdminList()
    {
      return View();
    }

  }
}