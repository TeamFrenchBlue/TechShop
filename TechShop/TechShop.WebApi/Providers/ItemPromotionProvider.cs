namespace TechShop.WebApi.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using TechShop.Models;
    using TechShop.Data.Data;
    using WebApi;

    public class ItemPromotionProvider : ItemDecorator<Product>
    {
        public ItemPromotionProvider(IEnumerable<Product> resultSet, ITechShopData data) 
            : base(resultSet, data)
        {
        }

        public override void Decorate()
        {

        }

        private IEnumerable<GlobalPromotion> GetActiveGlobalPromotions()
        {
           return this.Data.Categories.All().Wh
        }
    }
}