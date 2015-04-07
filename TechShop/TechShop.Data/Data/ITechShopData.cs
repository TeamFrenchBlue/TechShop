namespace TechShop.Data.Data
{
    using Models;
    using Repositories;

    public interface ITechShopData
    {
        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
