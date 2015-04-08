namespace TechShop.WebApi.Models.Admin
{
    public class AdminEditUserBindingModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool? IsAdmin { get; set; }
    }
}