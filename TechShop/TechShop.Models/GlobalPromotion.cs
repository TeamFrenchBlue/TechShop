namespace TechShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GlobalPromotion
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartedOn { get; set; }

        [Required]
        public DateTime EndedOn { get; set; }

        [Required]
        public virtual Promotion Promotion { get; set; }
    }
}
