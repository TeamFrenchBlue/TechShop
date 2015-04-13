namespace TechShop.Data.Context
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface ITechShopDbContext
    {
        IDbSet<User> Users { set; }

        IDbSet<UserSession> UserSessions { get; }

        IDbSet<Cart> Carts { get; }

        IDbSet<Category> Categories { get; }

        IDbSet<GlobalPromotion> GlobalPromotions { get; }

        IDbSet<IndividualPromotion> IndividualPromotions { get; }    

        IDbSet<Order> Orders { get; }

        IDbSet<Product> Products { get; }

        IDbSet<Promotion> Promotions { get; }

        IDbSet<State> States { get; }

        IDbSet<Trade> Trades { get; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
