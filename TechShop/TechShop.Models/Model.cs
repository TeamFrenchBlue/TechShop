namespace TechShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Model
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Trade Trade { get; set; }
    }
}
