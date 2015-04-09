namespace TechShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
