using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chefNDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 5)]
        public int Tastiness { get; set; }
        [Required]
        [Range(1, 2000)]
        public int Calories { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int ChefId {get;set;}
        public Chef CreatedBy { get; set; }
    }
}