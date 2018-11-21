using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTracker.Model;

namespace FoodTracker.Domain.Services.Interfaces
{
    public interface IIngredientsService
    {
        Task<Ingredient> CreateIngredient(Ingredient newIngredient);
        IEnumerable<Ingredient> GetAllIngredients();
    }
}
