namespace TechShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IndividualPromotion
    {
        public int Id { get; set; }

        public virtual Promotion Promotion { get; set; }

        public int PromotionId { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        [Required]
        public DateTime StartedOn { get; set; }

        [Required]
        public DateTime EndedOn { get; set; }
    }
}
