namespace TechShop.Data.Data
{
    using System;
    using System.Collections.Generic;

    using Context;
    using Models;
    using Repositories;

    public class TechShopData : ITechShopData
    {
        private ITechShopDbContext context;
        private IDictionary<Type, object> repositories;

        public TechShopData(ITechShopDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ITechShopDbContext Context
        {
            get { return this.context; }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<UserSession> UserSessions
        {
            get { return this.GetRepository<UserSession>(); }
        }

        public IRepository<Cart> Carts
        {
            get { return this.GetRepository<Cart>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); } 
            
        }

        public IRepository<GlobalPromotion> GlobalPromotions
        {
            get { return this.GetRepository<GlobalPromotion>(); }
        }

        public IRepository<IndividualPromotion> IndividualPromotions
        {
            get { return this.GetRepository<IndividualPromotion>(); }
            
        }

        public IRepository<Model> Models
        {
            get { return this.GetRepository<Model>(); }
        }

        public IRepository<Order> Orders
        {
            get { return this.GetRepository<Order>(); }
        }

        public IRepository<Product> Products
        {
            get { return this.GetRepository<Product>(); }
        }

        public IRepository<Promotion> Promotions
        {
            get { return this.GetRepository<Promotion>(); }
        }

        public IRepository<State> States
        {
            get { return this.GetRepository<State>(); }
        }

        public IRepository<Trade> Trades
        {
            get { return this.GetRepository<Trade>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var modelType = typeof(T);
            if (!this.repositories.ContainsKey(modelType))
            {
                var repositoryType = typeof(Repository<T>);
                this.repositories.Add(modelType, Activator.CreateInstance(repositoryType, this.context));
            }

            return (IRepository<T>)this.repositories[modelType];
        }
    }
}
