namespace FoodTracker.Model
{
    public class MealIngredient
    {
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
