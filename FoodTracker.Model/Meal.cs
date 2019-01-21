using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Model
{
    public class Meal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<MealIngredient> MealIngredients { get; set; }
        public DateTime Created { get; set; }
    }
}
