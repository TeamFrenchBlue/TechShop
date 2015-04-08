namespace TechShop.Data.Data
{
    using Models;
    using Repositories;

    public interface ITechShopData
    {
        IRepository<User> Users { get; }

        IRepository<UserSession> UserSessions { get; }
 
        int SaveChanges();
    }
}
