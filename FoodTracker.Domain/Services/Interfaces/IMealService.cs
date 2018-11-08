using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTracker.Model;

namespace FoodTracker.Domain.Services.Interfaces
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<IEnumerable<Meal>> GetAllMealsWithIngredientsAsync();
        Task CreateMeal(Meal meal);
    }
}
