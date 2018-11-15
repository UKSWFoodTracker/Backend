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
        private readonly FoodTrackerContext _context;

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
            var ingredients = mealDto.Ingredients.Select(i => _mapper.Map<IngredientDto, Ingredient>(i));

            await _mealService.CreateMealAsync(meal, ingredients);
        }

        [HttpPost]
        [Route("update")]
        public async Task UpdateMeal([FromBody] MealUpdateDto mealDto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Meal model is invalid");

            var meal = _mapper.Map<MealUpdateDto, Meal>(mealDto);
            var ingredients = mealDto.Ingredients.Select(i => _mapper.Map<IngredientDto, Ingredient>(i));

            await _mealService.UpdateMealAsync(meal, ingredients);
        }

        [HttpGet]
        [Route("delete/{mealId}")]
        public async Task DeleteMeal(int mealId)
        {
            await _mealService.DeleteMealAsync(mealId);
        }
    }
}