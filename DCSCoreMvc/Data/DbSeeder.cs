using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSCoreMvc.Data;
using DCSCoreMvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DCSCoreMvc.Data
{
  public class DbSeeder
  {
    #region Public Methods
    public static void Seed(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IHostingEnvironment env)
    {
      // Create default Users (if there are none)
      bool v = dbContext.Users.Any();
      if (!v) SeedUsers(dbContext, roleManager, userManager).GetAwaiter().GetResult();

    }
    #endregion

    public static async Task SeedUsers(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
      DateTimeOffset createdDate = DateTimeOffset.Now.AddDays(-10);
      DateTimeOffset lastModifiedDate = DateTimeOffset.Now;

      string[] roles = Roles.All();

      for (int i = 0; i < roles.Count(); i++)
      {
        if (!await roleManager.RoleExistsAsync(roles[i]))
        {
          await roleManager.CreateAsync(new IdentityRole(roles[i]));
        }
      }

      // create admin
      var admin = new ApplicationUser
      {
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = "admin",
        Email = "pieter.verhelle+admin@gmail.com",
      };

      ApplicationUser user = await userManager.FindByNameAsync(admin.UserName);
      if (user == null)
      {
        await userManager.CreateAsync(admin, "!GysluBXiwgEG6EV7Gj");
        await userManager.AddToRoleAsync(admin, Roles.Administrator);
        await userManager.AddToRoleAsync(admin, Roles.RegisteredUser);
        admin.EmailConfirmed = true;
        admin.LockoutEnabled = false;
      }

      await dbContext.SaveChangesAsync();
      // create admin
      var davina = new ApplicationUser
      {
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = "davina",
        Email = "pieter.verhelle+davina@gmail.com",
      };

      ApplicationUser user2 = await userManager.FindByNameAsync(davina.UserName);
      if (user == null)
      {
        await userManager.CreateAsync(davina, "!DavinaBrechtEnThor1");
        await userManager.AddToRoleAsync(davina, Roles.Administrator);
        await userManager.AddToRoleAsync(davina, Roles.RegisteredUser);
        davina.EmailConfirmed = true;
        davina.LockoutEnabled = false;
      }

      await dbContext.SaveChangesAsync();
    }
  }

  public sealed class Roles
  {

    public static readonly string Administrator = "Administrator";
    public static readonly string RegisteredUser = "RegisteredUser";
    public static readonly string Manager = "Manager";


    public static string[] All()
    {
      return new string[] { Administrator, RegisteredUser, Manager };
    }

  }
}
