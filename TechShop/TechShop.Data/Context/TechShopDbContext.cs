namespace TechShop.Data.Context
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class TechShopDbContext : IdentityDbContext<User>, ITechShopDbContext
    {
        public TechShopDbContext()
            : base("TechShopConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<TechShopDbContext, TechShopMigrationConfiguration>());
        }

        public IDbSet<UserSession> UserSessions { get; set; }

        public IDbSet<Cart> Carts { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<GlobalPromotion> GlobalPromotions { get; set; }

        public IDbSet<IndividualPromotion> IndividualPromotions { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Promotion> Promotions { get; set; }

        public IDbSet<State> States { get; set; }

        public IDbSet<Trade> Trades { get; set; }

        public static TechShopDbContext Create()
        {
            return new TechShopDbContext();
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasRequired(m => m.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(m => m.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(m => m.Trade)
                .WithMany(t => t.Products)
                .HasForeignKey(m => m.TradeId)
                .WillCascadeOnDelete(false);
        }
    }
}
