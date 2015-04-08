namespace TechShop.Data.Context
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface ITechShopDbContext
    {
        IDbSet<User> Users { set; }

        IDbSet<UserSession> UserSessions { get; } 

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
