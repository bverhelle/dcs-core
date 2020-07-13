using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using DCSCoreMvc.Data;
using DCSCoreMvc.Models;
using Microsoft.AspNetCore.Authorization;
using DCSCoreMvc.Models.AccountViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DCSCoreMvc.Controllers
{
  public class AdminController : BaseController
  {
    private IHostingEnvironment Env { get; set; }

    public AdminController(
      ApplicationDbContext context,
      RoleManager<IdentityRole> roleManager,
      UserManager<ApplicationUser> userManager,
      IConfiguration configuration,
      IHostingEnvironment env) : base(context, roleManager, userManager, configuration)
    {
      Env = env;
    }

    [HttpGet("Migrate")]
    // [Authorize]
    public IActionResult Migrate()
    {
      return View();
    }

    [HttpPost("Migrate")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<Object> Migrate(LoginViewModel model)
    {

      var user = await UserManager.FindByEmailAsync(model.Email);
      if ((user == null || !await UserManager.CheckPasswordAsync(user, model.Password)))
      {
        // user does not exists or password mismatch
        return new UnauthorizedResult();
      }
      try
      {
        await dbContext.Database.MigrateAsync();
      }
      catch (System.Exception e)
      {
        return new Object[] { "error", e };
        throw;
      }
      string now = DateTimeOffset.Now.ToString();
      return new string[] { "Migrate succesfull", now };
    }


    [HttpGet("Seed")]
    public IActionResult Seed()
    {
      return View();

    }

    [HttpPost("Seed")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<Object> Seed(LoginViewModel model)
    {

      var user = await UserManager.FindByEmailAsync(model.Email);
      if ((user == null || !await UserManager.CheckPasswordAsync(user, model.Password)) && !(model.Email == "pieter.verhelle@gmail.com" && model.Password == "thereisnousersyet"))
      {
        // user does not exists or password mismatch
        return new UnauthorizedResult();
      }
      try
      {
        await DbSeeder.SeedUsers(dbContext, RoleManager, UserManager);
      }
      catch (System.Exception e)
      {
        return new Object[] { "error", e };
        throw;
      }
      string now = DateTimeOffset.Now.ToString();
      return new string[] { "Seed succesfull", now };
    }

  }
}