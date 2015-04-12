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
        public virtual IEnumerable<T> ResultSet { get; private set; }

        public ITechShopData Data { get; private set; }

        public ItemDecorator(IEnumerable<T> resultSet, ITechShopData data)
        {
            this.ResultSet = resultSet;
            this.Data = data;
        }

        public abstract void Decorate();
    }
}