using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<Meal>> GetAsync()
        {
            var allMeals = await _mealService.GetAllMealsWithIngredientsAsync();

            return allMeals;
        }
    }
}