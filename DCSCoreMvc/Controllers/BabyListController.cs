using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSCoreMvc.Data;
using DCSCoreMvc.Data.Models;
using DCSCoreMvc.Models;
using DCSCoreMvc.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
          dbContext.Set<BabyListEntry>().Add(new BabyListEntry()
          {
            Email = model.Email,
            Name = model.Name,
            Address = model.Address,
            Nr = model.Nr,
            PostalCode = model.PostalCode,
            City = model.City,
            Phone = model.Phone,
            Client = isClient,
            CreatedDate = DateTimeOffset.Now
          });
          await dbContext.SaveChangesAsync();
          TempData["Email"] = model.Email;
          TempData["Name"] = model.Name;
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

    [HttpGet("Remove")]
    public IActionResult Remove()
    {
      return View();
    }

    [HttpPost("Remove")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<Object> Remove(RemoveFromListViewModel model)
    {

      var user = await UserManager.FindByEmailAsync(model.Email);
      if (user == null || !await UserManager.CheckPasswordAsync(user, model.Password))
      {
        // user does not exists or password mismatch
        return new UnauthorizedResult();
      }
      var removee = dbContext.Set<BabyListEntry>().FirstOrDefault(b => b.Email == model.EmailTooRemove);
      string now = DateTimeOffset.Now.ToString();
      var data = new object[] { "Removed Dataset", now, removee };

      dbContext.Entry(removee).State = EntityState.Deleted;
      await dbContext.SaveChangesAsync();
      return data;
    }


    [HttpGet("List/Prive")]
    // [Authorize]
    public async Task<Object> Prives()
    {
      return View();
    }

    [HttpPost("List/Prive")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<Object> Prives(LoginViewModel model)
    {

      var user = await UserManager.FindByEmailAsync(model.Email);
      if (user == null || !await UserManager.CheckPasswordAsync(user, model.Password))
      {
        // user does not exists or password mismatch
        return new UnauthorizedResult();
      }
      var d = dbContext.Set<BabyListEntry>().Where(b => !b.Client);

      string now = DateTimeOffset.Now.ToString();
      return new object[] { "Prives", now, d.Count(), d };
    }

    [HttpGet("List/Clients")]
    // [Authorize]
    public IActionResult Clients()
    {
      return View();
    }

    [HttpPost("List/Clients")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<Object> Clients(LoginViewModel model)
    {

      var user = await UserManager.FindByEmailAsync(model.Email);
      if (user == null || !await UserManager.CheckPasswordAsync(user, model.Password))
      {
        // user does not exists or password mismatch
        return new UnauthorizedResult();
      }
      var d = dbContext.Set<BabyListEntry>().Where(b => b.Client);

      string now = DateTimeOffset.Now.ToString();
      return new object[] { "Clients", now, d.Count(), d };
    }
  }
}