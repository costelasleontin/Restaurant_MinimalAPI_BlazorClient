using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace RestaurantsMinimalApi.Models
{
    public class MenuItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        [MinLength(3)]
        [MaxLength(1024)]
        [Required]
        public string? Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
    }
}