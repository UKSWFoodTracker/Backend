using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodTracker.Database;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.DTO;
using FoodTracker.Helpers.Exceptions;
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
        public async Task<MealDto> CreateMealAsync([FromBody] MealCreateDto mealDto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelException();

            var meal = _mapper.Map<MealCreateDto, Meal>(mealDto);
            var ingredients = mealDto.Ingredients.Select(i => _mapper.Map<IngredientDto, Ingredient>(i));

            await _mealService.CreateMealAsync(meal, ingredients);

            return _mapper.Map<MealDto>(meal);
        }

        [HttpPut]
        public async Task<MealDto> UpdateMealAsync([FromBody] MealUpdateDto mealDto)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelException();

            var meal = _mapper.Map<MealUpdateDto, Meal>(mealDto);
            var ingredients = mealDto.Ingredients.Select(i => _mapper.Map<IngredientDto, Ingredient>(i));

            await _mealService.UpdateMealAsync(meal, ingredients);
            var updatedMeal = await _mealService.GetMealByIdAsync(meal.Id);
            return _mapper.Map<MealDto>(updatedMeal);
        }

        [HttpDelete]
        [Route("{mealId}")]
        public async Task<MealDto> DeleteMealAsync(int mealId)
        {
            var selectedMeal = await _mealService.GetMealByIdAsync(mealId);
            var dto = _mapper.Map<MealDto>(selectedMeal);

            await _mealService.DeleteMealAsync(mealId);

            return dto;
        }
    }
}