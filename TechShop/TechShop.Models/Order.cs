namespace TechShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        public Guid Id { get; set; }

        public DateTime OrderedOn { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        
        public Order()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
