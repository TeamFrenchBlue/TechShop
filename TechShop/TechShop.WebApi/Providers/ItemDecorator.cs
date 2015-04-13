namespace TechShop.WebApi.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using TechShop.Models;
    using TechShop.Data.Data;
    using WebApi;

    public abstract class ItemDecorator<T>
    {
        protected ICollection<T> resultSet;

        public virtual ICollection<T> ResultSet
        { 
            get
            {
                return this.resultSet;
            }
            private set
            {
                this.resultSet = value;
            }
        }

        public ITechShopData Data { get; private set; }

        public ItemDecorator(IEnumerable<T> resultSet, ITechShopData data)
        {
            this.ResultSet = resultSet.ToList();
            this.Data = data;
        }

        public abstract void Decorate();
    }
}