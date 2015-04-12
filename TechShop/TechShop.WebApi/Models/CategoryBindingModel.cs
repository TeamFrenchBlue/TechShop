namespace TechShop.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using TechShop.Models;

    public class CategoryBindingModel
    {
        public static Expression<Func<Category, CategoryBindingModel>> FromCategory
        {
            get
            {
                return c => new CategoryBindingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Position = c.Position
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Position { get; set; }
    }
}