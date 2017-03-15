namespace CarSharing.Migrations
{
    using CarSharing.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<CarSharing.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarSharing.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            Microsoft.AspNet.Identity.EntityFramework.IdentityRole ruolo = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Admin");
            context.Roles.AddOrUpdate(p => p.Name, new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Admin" },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "User" }
                );
            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();


            //const string username = "admin";
            //const string email = "admin@example.com";
            //const string password = "Admin@123456";
            //const string name = "Administrator";
            //const string surname = "Global";
            //const string roleName = "Admin";

            //var role = roleManager.FindByName(roleName);
            //if (role == null)
            //{
            //    role = new IdentityRole(roleName);
            //    var roleresult = roleManager.Create(role);
            //}

            //if(context.Users.Count() == 0)
            //{
            //    var user = new ApplicationUser { UserName = username, Email = email, Name = name, Surname = surname };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //    var rolesForUser = userManager.GetRoles(user.Id);
            //    if (!rolesForUser.Contains(role.Name))
            //    {
            //        result = userManager.AddToRole(user.Id, role.Name);
            //    }
            //}


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
