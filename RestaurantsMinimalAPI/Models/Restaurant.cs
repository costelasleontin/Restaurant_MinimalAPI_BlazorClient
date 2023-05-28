using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace RestaurantsMinimalApi.Models
{
    public class Restaurant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [StringLength(100)]
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Schedule { get; set; } 

        [DataType(DataType.Currency)]
        [Required]
        public decimal MinimumOrderPrice { get; set; }

        [Required]
        public int MaximumDistanceStdDelivery { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal StdDeliveryPrice { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal ExtraDeliveryFeePerKm { get; set; }

        public ICollection<MenuItem>? MenuItems { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}