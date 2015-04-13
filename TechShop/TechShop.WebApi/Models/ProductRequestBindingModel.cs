namespace TechShop.WebApi.Models
{
    public class ProductRequestBindingModel
    {
        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public int? TradeId { get; set; }

        public decimal? FromPrice { get; set; }

        public decimal? ToPrice { get; set; }

        public bool? InPromotion { get; set; }
    }
}