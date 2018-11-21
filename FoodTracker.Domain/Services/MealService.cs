using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTracker.Database;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Domain.Services
{
    public class MealService : IMealService
    {
        private readonly FoodTrackerContext _context;
        private readonly IIngredientsService _ingredientsService;

        public MealService(FoodTrackerContext context, IIngredientsService ingredientsService)
        {
            _context = context;
            _ingredientsService = ingredientsService;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            var meals = await _context.Meals.ToListAsync();
            return meals;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsWithIngredientsAsync()
        {
            var mealsWithIngredients = await _context.Meals
                .Include(m => m.MealIngredients).ThenInclude(mi => mi.Ingredient)
                .ToListAsync();

            return mealsWithIngredients;
        }

        public async Task CreateMealAsync(Meal meal, IEnumerable<Ingredient> ingredients)
        {
            await _context.AddAsync(meal);
            _context.SaveChanges();

            foreach (var ingredient in ingredients)
            {
                var createIngredient = await _ingredientsService.CreateIngredient(ingredient);

                var mealIngredient = new MealIngredient { MealId = meal.Id, IngredientId = createIngredient.Id };
                await _context.MealIngredients.AddAsync(mealIngredient);
            }

            _context.SaveChanges();
        }

        public async Task DeleteMealAsync(int mealId)
        {
            if(!_context.Meals.Any(m => m.Id == mealId))
                throw new Exception("There is no meal with given id");

            var trashMeal = await _context.Meals.SingleAsync(m => m.Id == mealId);

            _context.Remove(trashMeal);
            _context.SaveChanges();
        }

        public async Task UpdateMealAsync(Meal updateMeal, IEnumerable<Ingredient> updateIngredients)
        {
            if(!_context.Meals.Any(m => m.Id == updateMeal.Id))
                throw new Exception("There is no meal with given id");

            var oldMeal = await _context.Meals.SingleAsync(m => m.Id == updateMeal.Id);
            oldMeal.Name = updateMeal.Name;

            var trashMealIngredients = _context.MealIngredients.Where(mi => mi.MealId == updateMeal.Id);
            _context.MealIngredients.RemoveRange(trashMealIngredients);

            foreach (var ingredient in updateIngredients)
            {
                var createIngredient = await _ingredientsService.CreateIngredient(ingredient);

                var mealIngredient = new MealIngredient { MealId = updateMeal.Id, IngredientId = createIngredient.Id };
                await _context.MealIngredients.AddAsync(mealIngredient);
            }

            _context.SaveChanges();
        }
    }
}
