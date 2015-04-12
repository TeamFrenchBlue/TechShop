﻿namespace TechShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Trade
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Position { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
