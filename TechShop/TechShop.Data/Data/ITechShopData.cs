namespace TechShop.Data.Data
{
    using Models;
    using Repositories;

    public interface ITechShopData
    {
        IRepository<User> Users { get; }

        IRepository<UserSession> UserSessions { get; }


        IRepository<Cart> Carts { get; }

        IRepository<Category> Categories { get; }

        IRepository<GlobalPromotion> GlobalPromotions { get; }

        IRepository<IndividualPromotion> IndividualPromotions { get; }      

        IRepository<Order> Orders { get; }

        IRepository<Product> Products { get; }

        IRepository<Promotion> Promotions { get; }

        IRepository<State> States { get; }

        IRepository<Trade> Trades { get; }
 
        int SaveChanges();
    }
}
