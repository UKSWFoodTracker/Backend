using System;
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

        [HttpPost]
        [Route("add")]
        public async Task AddMeal([FromBody] Meal meal)
        {
            if (!ModelState.IsValid)
                throw new Exception("Meal model is invalid");

            await _mealService.CreateMeal(meal);
        }

        [HttpGet]
        [Route("mock")]
        public Meal GetEmpty()
        {
            var meal = new Meal()
            {
                Id = 1,
                MealIngredients = new List<MealIngredient>()
            };

            var ingredient = new Ingredient
            {
                Id = 1,
                Calories = 100,
                Name = "Fish",
                MealIngredients = new List<MealIngredient>()
            };

            var mealIngredient = new MealIngredient {IngredientId = 1, Ingredient = ingredient, Meal = meal, MealId = 1};

            meal.MealIngredients.Add(mealIngredient);
            ingredient.MealIngredients.Add(mealIngredient);

            return meal;
        }
    }
}