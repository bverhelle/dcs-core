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
  public class BabyLijstController : BaseController
  {

    public BabyLijstController(
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
      return RedirectToAction("Index", "BabyList");
    }

    [HttpGet("Prive")]
    [AllowAnonymous]
    public IActionResult Prive()
    {
      return RedirectToAction("Prive", "BabyList");
    }

  }
}