using System.Collections.Generic;

namespace FoodTracker.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public ICollection<MealIngredient> MealIngredients { get; set; }
    }
}
