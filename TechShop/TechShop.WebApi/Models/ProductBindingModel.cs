namespace TechShop.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using TechShop.Models;

    public class ProductBindingModel
    {
        public static Expression<Func<Product, ProductBindingModel>> FromProduct
        {
            get
            {
                return p => new ProductBindingModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    TradeId = p.TradeId,
                    Price = p.Price,
                    PromotionPrice = p.Price,
                    Quantity = p.Quantity,
                    TradeName = p.Trade.Name.ToLower(),
                    CategoryName = p.Category.Name.ToLower()
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int TradeId { get; set; }

        public string TradeName { get; set; }

        public decimal Price { get; set; }

        public decimal PromotionPrice { get; set; }

        public double Discount { get; set; }

        public int Quantity { get; set; }
    }
}