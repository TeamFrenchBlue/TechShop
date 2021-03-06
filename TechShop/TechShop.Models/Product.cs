﻿namespace TechShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        private ICollection<IndividualPromotion> promotions;

        public Product()
        {
            this.promotions = new HashSet<IndividualPromotion>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int TradeId { get; set; }

        public virtual Trade Trade { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<IndividualPromotion> Promotions
        {
            get { return this.promotions; }
            set { this.promotions = value; }
        }
    }
}
