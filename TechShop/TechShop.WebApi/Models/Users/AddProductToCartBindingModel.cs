namespace TechShop.WebApi.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class AddProductToCartBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}