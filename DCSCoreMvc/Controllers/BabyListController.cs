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

    [HttpGet("Prive")]
    [AllowAnonymous]
    public IActionResult Prive()
    {
      return RedirectToAction("Index", new { c = "0" });
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index(string c)
    {

      TempData["IsClient"] = c;
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
          var isClient = TempData["IsClient"] as string == "0" ? false : true;
          dbContext.Set<BabyListEntry>().Add(new BabyListEntry() { Email = model.Email, Name = model.Name, Client = isClient, CreatedDate = DateTimeOffset.Now });
          await dbContext.SaveChangesAsync();
          TempData["Name"] = model.Name;
          TempData["Email"] = model.Email;
          TempData["Address"] = model.Address;
          TempData["Nr"] = model.Nr;
          TempData["PostalCode"] = model.PostalCode;
          TempData["City"] = model.City;
          TempData["Phone"] = model.Phone;
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
      ViewData["Name"] = TempData["Name"];
      ViewData["Email"] = TempData["Email"];
      ViewData["Address"] = TempData["Address"];
      ViewData["Nr"] = TempData["Nr"];
      ViewData["PostalCode"] = TempData["PostalCode"];
      ViewData["City"] = TempData["City"];
      ViewData["Phone"] = TempData["Phone"];

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

    [HttpGet("List/Clients")]
    // [Authorize]
    public async Task<Object> Clients()
    {
      var d = dbContext.Set<BabyListEntry>().Where(b => b.Client);

      string now = DateTimeOffset.Now.ToString();
      return new object[] { "Clients", now, d.Count(), d };
    }
    [HttpGet("List/Prive")]
    // [Authorize]
    public async Task<Object> Prives()
    {
      var d = dbContext.Set<BabyListEntry>().Where(b => !b.Client);

      string now = DateTimeOffset.Now.ToString();
      return new object[] { "Prives", now, d.Count(), d };
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
          var isClient = TempData["IsClient"] as string == "0" ? false : true;
          dbContext.Set<BabyListEntry>().Add(new BabyListEntry() { Email = model.Email, Name = model.Name, Client = isClient, CreatedDate = DateTimeOffset.Now });
          await dbContext.SaveChangesAsync();
          TempData["Name"] = model.Name;
          TempData["Email"] = model.Email;
          TempData["Address"] = model.Address;
          TempData["Nr"] = model.Nr;
          TempData["PostalCode"] = model.PostalCode;
          TempData["City"] = model.City;
          TempData["Phone"] = model.Phone;
          return RedirectToAction(nameof(Enlisted));
        }
        if (alreadyEnlisted)
        {
          return RedirectToAction(nameof(AlreadyEnlisted));
        }

      }

      return View(model);
    }
  }
}