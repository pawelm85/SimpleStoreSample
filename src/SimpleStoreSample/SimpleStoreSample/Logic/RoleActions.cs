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
                if(!IdRoleResult.Succeeded)
                {
                    // Handle error.
                }
            }
        }
    }
}