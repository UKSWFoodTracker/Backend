using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodTracker.Database;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.DTO;
using FoodTracker.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;
        private FoodTrackerContext _context;

        public MealController(IMealService mealService, IMapper mapper, FoodTrackerContext context)
        {
            _mealService = mealService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<MealDto>> GetAsync()
        {
            var allMeals = await _mealService.GetAllMealsWithIngredientsAsync();

            return allMeals.Select(m => _mapper.Map<MealDto>(m));
        }

        [HttpPost]
        [Route("add")]
        public async Task AddMeal([FromBody] MealDto mealDto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Meal model is invalid");

            var meal = _mapper.Map<MealDto, Meal>(mealDto);

            await _context.AddAsync(meal);
            _context.SaveChanges();

            foreach (var ingredientDto in mealDto.Ingredients)
            {
                int ingredientId;
                if (_context.Ingredients.Any(i => i.Name == ingredientDto.Name))
                {
                    ingredientId = _context.Ingredients
                        .Where(i => i.Name == ingredientDto.Name)
                        .Select(i => i.Id)
                        .Single();
                }
                else
                {
                    var ingredient = _mapper.Map<IngredientDto, Ingredient>(ingredientDto);
                    await _context.Ingredients.AddAsync(ingredient);
                    _context.SaveChanges();

                    ingredientId = ingredient.Id;
                }

                var mealIngredient = new MealIngredient {MealId = meal.Id, IngredientId = ingredientId };

                await _context.MealIngredients.AddAsync(mealIngredient);
            }

            _context.SaveChanges();
        }

        [HttpGet]
        [Route("mock")]
        public MealDto GetEmpty()
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

            return _mapper.Map<MealDto>(meal);
        }
    }
}