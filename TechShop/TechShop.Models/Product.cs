namespace TechShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public virtual Model Model { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
