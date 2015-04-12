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
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TechShopDbContext context)
        {
            this.AddAdminUser(context);
            if (!context.Products.Any())
            {
                this.AddCategories(context);
                this.AddTrades(context);
                this.AddModels(context);
                this.AddProducts(context); 
                this.AddOrderStates(context);
            }
        }

        private void AddAdminUser(TechShopDbContext context)
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

        private void AddProducts(TechShopDbContext context)
        {
            var lenovoLaptop = new Product
            {
                Model = context.Models.First(m => m.Name == "Lenovo E450"),
                Price = 980.50m,
                Description =
                    @"Explore the best 14"" business solution with next generation graphics,
                        great storage, 5-button clickpad, and improved mobility"
            };

            context.Products.Add(lenovoLaptop);
            context.SaveChanges();
        }

        private void AddCategories(TechShopDbContext context)
        {
            var laptopsCategory = new Category
            {
                Name = "Laptops"
            };

            var phonesCategory = new Category
            {
                Name = "Phones"
            };

            context.Categories.AddOrUpdate(laptopsCategory, phonesCategory);
            context.SaveChanges();
        }

        private void AddTrades(TechShopDbContext context)
        {
            var nokiaTrade = new Trade
            {
                Name = "Nokia",
                Category = context.Categories.First(c => c.Name == "Phones")
            };

            var lenovoTrade = new Trade
            {
                Name = "Lenovo",
                Category = context.Categories.First(c => c.Name == "Laptops")
            };

            context.Trades.AddOrUpdate(nokiaTrade, lenovoTrade);
            context.SaveChanges();
        }

        private void AddModels(TechShopDbContext context)
        {
            var lenovoModel = new Model
            {
                Name = "Lenovo E450",
                Trade = context.Trades.First(t => t.Name == "Lenovo")
            };

            var nokiaModel = new Model
            {
                Name = "Nokia E6",
                Trade = context.Trades.First(t => t.Name == "Nokia")
            };

            context.Models.AddOrUpdate(lenovoModel, nokiaModel);
            context.SaveChanges();
        }

        public void AddOrderStates(TechShopDbContext context)
        {
            var waitingApprovalState = new State
            {
                Name = "Waiting approval"
            };

            var paidState = new State
            {
                Name = "Paid"
            };

            context.States.AddOrUpdate(waitingApprovalState, paidState);
            context.SaveChanges();
        }
    }
}
