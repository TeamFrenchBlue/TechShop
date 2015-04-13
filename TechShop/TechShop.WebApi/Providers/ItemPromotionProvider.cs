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
    using TechShop.WebApi.Models;

    public class ItemPromotionProvider : ItemDecorator<ProductBindingModel>
    {
        public override ICollection<ProductBindingModel> ResultSet
        {
            get
            {
                return this.resultSet;
            }
        }


        private const double MaximumAllowedDiscount = 0.8;

        public ItemPromotionProvider(IEnumerable<ProductBindingModel> resultSet, ITechShopData data)
            : base(resultSet, data)
        {
            foreach (var product in resultSet)
            {
                var localProduct = new ProductBindingModel();
                localProduct.Id = product.Id;
                localProduct.ImageUrl = product.ImageUrl;
                localProduct.TradeId = product.TradeId;
                localProduct.Price = product.Price;
                localProduct.Description = product.Description;
                localProduct.Discount = 0.00;
                localProduct.PromotionPrice = product.Price;
                localProduct.CategoryId = product.CategoryId;

                this.ResultSet.Add(localProduct);
            }
        }

        public override void Decorate()
        {
            var globalPromotionDiscountModifier = 0.00;
            var promoted = false;
            if (this.GetActiveGlobalPromotions() != null && this.GetActiveGlobalPromotions().Count() > 0)
            {
                promoted = true;
                foreach (var globalPromotion in this.GetActiveGlobalPromotions())
                {
                    var localModifier = globalPromotion.Promotion.Discount / 100;
                    globalPromotionDiscountModifier += localModifier;
                }
            }

            globalPromotionDiscountModifier = (globalPromotionDiscountModifier <= MaximumAllowedDiscount)
                ? globalPromotionDiscountModifier
                : MaximumAllowedDiscount;

            foreach (var product in this.ResultSet)
            {
                var productPromotionDiscountModifier = globalPromotionDiscountModifier;
                var productPromotions = this.GetActiveProductPromotion(product.Id);
                if (productPromotions != null && productPromotions.Count() > 0)
                {
                    promoted = true;
                    foreach (var productPromotion in productPromotions)
                    {
                        var localModifier = productPromotion.Promotion.Discount / 100;
                        productPromotionDiscountModifier += localModifier;
                    }
                }

                productPromotionDiscountModifier = (productPromotionDiscountModifier <= MaximumAllowedDiscount)
                    ? productPromotionDiscountModifier
                    : MaximumAllowedDiscount;

                if (promoted)
                {
                    product.PromotionPrice -= product.PromotionPrice * (decimal)productPromotionDiscountModifier;
                    product.Discount = productPromotionDiscountModifier * 100;
                }
            }

        }

        private IEnumerable<GlobalPromotion> GetActiveGlobalPromotions()
        {
            return this.Data.GlobalPromotions.All()
                .Where(p => p.StartedOn <= DateTime.Now)
                .Where(p => p.EndedOn > DateTime.Now);
        }

        private IEnumerable<IndividualPromotion> GetActiveProductPromotion(int productId)
        {
            return this.Data.IndividualPromotions.All()
                .Where(p => p.ProductId == productId)
                .Where(p => p.StartedOn <= DateTime.Now)
                .Where(p => p.EndedOn > DateTime.Now);
        }
    }
}