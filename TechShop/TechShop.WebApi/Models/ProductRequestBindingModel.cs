namespace TechShop.WebApi.Models
{
    public class ProductRequestBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public int? TradeId { get; set; }

        public string CategoryName { get; set; }

        public string TradeName { get; set; }

        public decimal? FromPrice { get; set; }

        public decimal? ToPrice { get; set; }

        public bool? InPromotion { get; set; }
    }
}