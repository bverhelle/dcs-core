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
    public async Task<Object> Migrate()
    {
      // var isAdmin = await CurrentUserIsAdmin();
      // if (!isAdmin)
      // {
      //   return new UnauthorizedResult();
      // }
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
    public async Task<Object> Seed()
    {
      // var isAdmin = await CurrentUserIsAdmin();
      // if (!isAdmin)
      // {
      //   return new UnauthorizedResult();
      // }
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

    [HttpPost("Seed")]


  }
}