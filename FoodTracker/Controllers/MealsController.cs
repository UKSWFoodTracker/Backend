using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodTracker.Database;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.DTO;
using FoodTracker.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;

        public MealsController(IMealService mealService, IMapper mapper, FoodTrackerContext context)
        {
            _mealService = mealService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MealDto>> GetAllMealsAsync()
        {
            var allMeals = await _mealService.GetAllMealsWithIngredientsAsync();

            var dtos = allMeals.Select(m => _mapper.Map<Meal, MealDto>(m));

            return dtos;
        }

        [HttpPost]
        public async Task CreateMealAsync([FromBody] MealCreateDto mealDto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Meal model is invalid");

            var meal = _mapper.Map<MealCreateDto, Meal>(mealDto);
            var ingredients = mealDto.Ingredients.Select(i => _mapper.Map<IngredientDto, Ingredient>(i));

            await _mealService.CreateMealAsync(meal, ingredients);
        }

        [HttpPut]
        public async Task UpdateMealAsync([FromBody] MealUpdateDto mealDto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Meal model is invalid");

            var meal = _mapper.Map<MealUpdateDto, Meal>(mealDto);
            var ingredients = mealDto.Ingredients.Select(i => _mapper.Map<IngredientDto, Ingredient>(i));

            await _mealService.UpdateMealAsync(meal, ingredients);
        }

        [HttpDelete]
        [Route("{mealId}")]
        public async Task DeleteMealAsync(int mealId)
        {
            await _mealService.DeleteMealAsync(mealId);
        }
    }
}