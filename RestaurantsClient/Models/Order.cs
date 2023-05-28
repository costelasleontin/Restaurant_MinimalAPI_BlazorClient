using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace RestaurantsClient.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        [MinLength(3)]
        [StringLength(512)]
        [Required]
        public string? Address { get; set; }

        [Range(0, 200)]
        [Required]
        public int DistanceInKm { get; set; }

        [StringLength(1024)]
        public string? OtherMentions { get; set; }

        public int RestaurantId { get; set; }
        // public Restaurant? Restaurant { get; set; }

        //for brevity we use a string and not a separate OrderList model to save order items
        public string OrderList { get; set; } = "";
    }
}