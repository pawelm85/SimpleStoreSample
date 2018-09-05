using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleStoreSample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SimpleStoreSample.Logic
{
    internal class RoleActions
    {
        internal void CreateAdmin()
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create RoleStore object by using the ApplicationDbContext object.
            // The RoleStore is only allowed to contain ItentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            var roleMgr = new RoleManager<IdentityRole>(roleStore);
            if(!roleMgr.RoleExists("Administrator"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole("Administrator"));
            }

            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = "admin@simplesamplestore.com",
                Email = "admin@simplesamplestore.com"
            };
            IdUserResult = userMgr.Create(appUser, "Pa$$word");

            if(IdUserResult.Succeeded)
            {
                IdUserResult = userMgr.AddToRole(appUser.Id, "Administrator");
               
            }
           
        }
    }
}