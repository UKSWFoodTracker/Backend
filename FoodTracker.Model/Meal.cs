using System.Collections;
using System.Collections.Generic;

namespace FoodTracker.Model
{
    public class Meal
    {
        public int Id { get; set; }
        public ICollection<MealIngredient> MealIngredients { get; set; }
    }
}
