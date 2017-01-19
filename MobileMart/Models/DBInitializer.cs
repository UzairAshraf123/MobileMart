using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MobileMart.Models
{
    public class DBInitializer:CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            List<IdentityRole> identityRoles = new List<IdentityRole>();
            identityRoles.Add(new IdentityRole() { Name = "Admin" });
            identityRoles.Add(new IdentityRole() { Name = "ShopKeeper" });
            identityRoles.Add(new IdentityRole() { Name = "Customer" });

            foreach (IdentityRole role in identityRoles)
            {
                roleManager.Create(role);
            }

            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = new ApplicationUser();
            user.Email = "admin123@admin.com";
            user.UserName = "admin123@admin.com";

            manager.Create(user, "admin123");
            manager.AddToRole(user.Id, "Admin");

            base.Seed(context);
        }
    }
}