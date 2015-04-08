namespace TechShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Context;
    using Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class TechShopMigrationConfiguration
        : DbMigrationsConfiguration<TechShopDbContext>
    {
        public TechShopMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TechShopDbContext context)
        {
            var adminExists = context.Users.FirstOrDefault(u => u.UserName == "admin") != null;
            if (!adminExists)
            {
                var userManager = new UserManager<User>(new UserStore<User>(context));

                // Create admin user
                var admin = new User
                {
                    Name = "Admin",
                    UserName = "admin",
                    Email = "admin@abv.bg"
                };
                var adminPassword = "admin123";
                var adminUserCreateResult = userManager.Create(admin, adminPassword);
                if (!adminUserCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", adminUserCreateResult.Errors));
                }

                // Create "Administrator" role
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleCreateResult = roleManager.Create(new IdentityRole("Administrator"));
                if (!roleCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleCreateResult.Errors));
                }

                // Add admin user to "Administrator" role
                var adminUser = context.Users.First(user => user.UserName == "admin");
                var addAdminRoleResult = userManager.AddToRole(adminUser.Id, "Administrator");
                if (!addAdminRoleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
                }
            }
        }
    }
}
