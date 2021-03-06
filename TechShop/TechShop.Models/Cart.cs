﻿namespace TechShop.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual State State { get; set; }

        public virtual Order Order { get; set; }
    }
}
