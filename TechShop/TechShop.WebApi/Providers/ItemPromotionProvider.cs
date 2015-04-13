namespace TechShop.WebApi.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Linq;

    using TechShop.Models;
    using TechShop.Data.Data;
    using WebApi;

    //public class ItemPromotionProvider : ItemDecorator<Product>
    //{
    //    public override IEnumerable<ProductResultSet> ResultSet
    //    {
    //        get;
    //        private set;
    //    }


    //    private const double MaximumAllowedDiscount = 0.8;

    //    public ItemPromotionProvider(IEnumerable<Product> resultSet, ITechShopData data)
    //        : base(resultSet, data)
    //    {
    //        foreach (var product in resultSet)
    //        {
    //            foreach (var localProduct in this.ResultSet)
    //            {
    //                localProduct.Id = product.Id;
    //                localProduct.ImageUrl = product.ImageUrl;
    //                localProduct.Model = product.Model;
    //                localProduct.RawPrice = product.Price;
    //                localProduct.Description = product.Description;
    //                localProduct.Discount = 0.00;
    //                localProduct.PromotedPrice = product.Price;
    //            }
    //        }
    //    }

    //    public override void Decorate()
    //    {
    //        var globalPromotionDiscountModifier = 0.00;
    //        if (this.GetActiveGlobalPromotions() != null && this.GetActiveGlobalPromotions().Count() > 0)
    //        {
    //            foreach (var globalPromotion in this.GetActiveGlobalPromotions())
    //            {
    //                var localModifier = globalPromotion.Promotion.Discount / 100;
    //                globalPromotionDiscountModifier += localModifier;
    //            }
    //        }

    //        globalPromotionDiscountModifier = (globalPromotionDiscountModifier <= MaximumAllowedDiscount)
    //            ? globalPromotionDiscountModifier
    //            : MaximumAllowedDiscount;

    //        foreach (var product in this.ResultSet)
    //        {
    //            var productPromotionDiscountModifier = globalPromotionDiscountModifier;
    //            var productPromotions = this.GetActiveProductPromotion(product.Id);
    //            if (productPromotions != null && productPromotions.Count() > 0)
    //            {
    //                foreach (var productPromotion in productPromotions)
    //                {
    //                    var localModifier = productPromotion.Promotion.Discount / 100;
    //                    productPromotionDiscountModifier += localModifier;
    //                }
    //            }

    //            productPromotionDiscountModifier = (productPromotionDiscountModifier <= MaximumAllowedDiscount)
    //                ? productPromotionDiscountModifier
    //                : MaximumAllowedDiscount;

    //            product.PromotedPrice *= (decimal)productPromotionDiscountModifier;
    //            product.Discount = productPromotionDiscountModifier * 100;
    //        }

    //    }

    //    private IEnumerable<GlobalPromotion> GetActiveGlobalPromotions()
    //    {
    //        return this.Data.GlobalPromotions.All()
    //            .Where(p => p.StartedOn >= DateTime.Now)
    //            .Where(p => p.EndedOn < DateTime.Now);
    //    }

    //    private IEnumerable<IndividualPromotion> GetActiveProductPromotion(int productId)
    //    {
    //        return this.Data.IndividualPromotions.All()
    //            .Where(p => p.ProductId == productId)
    //            .Where(p => p.StartedOn >= DateTime.Now)
    //            .Where(p => p.EndedOn < DateTime.Now);
    //    }
    //}

    internal class ProductResultSet
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

       // public virtual Model Model { get; set; }

        public decimal RawPrice { get; set; }

        public decimal PromotedPrice { get; set; }

        public double Discount { get; set; }
    }
}