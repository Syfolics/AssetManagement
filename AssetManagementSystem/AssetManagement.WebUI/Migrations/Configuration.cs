namespace AssetManagement.WebUI.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AssetManagement.WebUI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AssetManagement.WebUI.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "SuperAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "SuperAdmin" };

                manager.Create(role);

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var PasswordHash = new PasswordHasher();

                if (!context.Users.Any(u => u.UserName == "helpdesk@admin.net"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "helpdesk@admin.net",
                        Email = "helpdesk@admin.net",
                        PasswordHash = PasswordHash.HashPassword("123456")
                    };

                    UserManager.Create(user);
                    UserManager.AddToRole(user.Id, "SuperAdmin");
                }
            }

        }
    }
}
