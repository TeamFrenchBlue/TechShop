namespace TechShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Promotion
    {
        public int Id { get; set; }

        [Required]
        public double Discount { get; set; }
    }
}
