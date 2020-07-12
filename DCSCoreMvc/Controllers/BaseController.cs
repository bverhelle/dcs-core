using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSCoreMvc.Data;
using DCSCoreMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DCSCoreMvc.Controllers
{

  [Route("[controller]")]
  public class BaseController : Controller
  {
    public BaseController(
        ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
      dbContext = context;
      RoleManager = roleManager;
      UserManager = userManager;
      Configuration = configuration;
    }

    protected async Task<bool> IsAuthorized(ApplicationUser owner)
    {
      var currentUser = await CurrentUser();
      if (owner == null || !owner.Id.Equals(currentUser.Id)) return false;
      return true;
    }

    protected async Task<ApplicationUser> CurrentUser()
    {
      return await UserManager.GetUserAsync(User);
    }

    protected async Task<bool> CurrentUserIsAdmin()
    {
      var user = await CurrentUser();
      return user.UserName == "noor" || user.UserName == "admin";
    }

    protected ApplicationDbContext dbContext { get; private set; }
    protected RoleManager<IdentityRole> RoleManager { get; private set; }
    protected UserManager<ApplicationUser> UserManager { get; private set; }
    protected IConfiguration Configuration { get; private set; }
  }
}