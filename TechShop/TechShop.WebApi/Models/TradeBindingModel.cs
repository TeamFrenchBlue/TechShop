namespace TechShop.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using TechShop.Models;

    public class TradeBindingModel
    {
        public static Expression<Func<Trade, TradeBindingModel>> FromTrade
        {
            get
            {
                return t => new TradeBindingModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Position = t.Position                    
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Position { get; set; }
    }
}