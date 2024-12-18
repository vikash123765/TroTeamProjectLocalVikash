using System.ComponentModel.DataAnnotations;

namespace MovieShopTrio.Database
{
    public class Customer
    {
        public int Id {get; set;}

        [Required]
        [StringLength(100)]
        public string Name {get; set;}

        [Required]
        [Display(Name = "Billing Address")]
        public string BillingAddress {get; set;}

        [Required]
        [Display(Name = "Billing City")]
        public string BillingCity {get; set;}

        [Required]
        [Display(Name= "Billing Zip")]
        public string BillingZip {get; set;}

        [Required]
        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        [Required]
        [Display(Name = "Delivery City")]
        public string DeliveryCity {get; set;}

        [Required]
        [Display(Name="Delivery Zip")]
        public string DeliveryZip { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();


    }
}
