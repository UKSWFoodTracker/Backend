using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTracker.Model;

namespace FoodTracker.Domain.Services.Interfaces
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<IEnumerable<Meal>> GetAllMealsWithIngredientsAsync();
        Task DeleteMealAsync(int mealId);
        Task CreateMealAsync(Meal meal, IEnumerable<Ingredient> ingredients);
        Task UpdateMealAsync(Meal updateMeal, IEnumerable<Ingredient> updateIngredients);
    }
}
