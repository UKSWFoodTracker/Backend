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
        public async Task<ActionResult<IEnumerable<MealDto>>> GetAllMealsAsync()
        {
            var allMeals = await _mealService.GetAllMealsWithIngredientsAsync();

            var dtos = allMeals.Select(m => _mapper.Map<Meal, MealDto>(m));

            return new OkObjectResult(dtos);
        }

        [HttpGet]
        [Route("{mealId}")]
        public async Task<ActionResult<MealDto>> GetMealByIdAsync(int mealId)
        {
            var selectedMeal = await _mealService.GetMealByIdAsync(mealId);
            if (selectedMeal == null)
                return NotFound();

            var dto = _mapper.Map<MealDto>(selectedMeal);
            return dto;
        }

        [HttpPost]
        public async Task<ActionResult<MealDto>> CreateMealAsync([FromBody] MealCreateDto mealDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var meal = _mapper.Map<MealCreateDto, Meal>(mealDto);
            var ingredients = mealDto.Ingredients.Select(i => _mapper.Map<IngredientDto, Ingredient>(i));

            await _mealService.CreateMealAsync(meal, ingredients);

            var dto = _mapper.Map<MealDto>(meal);
            return CreatedAtAction(nameof(GetMealByIdAsync), new { mealId = dto.Id }, dto);
        }

        [HttpPut]
        public async Task<ActionResult<MealDto>> UpdateMealAsync([FromBody] MealUpdateDto mealDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var oldMeal = await _mealService.GetMealByIdAsync(mealDto.Id);
            if (oldMeal == null)
                return NotFound();

            var mealModel = _mapper.Map<MealUpdateDto, Meal>(mealDto);
            var ingredientsModel = mealDto.Ingredients.Select(i => _mapper.Map<IngredientDto, Ingredient>(i));

            await _mealService.UpdateMealAsync(mealModel, ingredientsModel);

            var updatedMeal = await _mealService.GetMealByIdAsync(mealModel.Id);
            var dto = _mapper.Map<MealDto>(updatedMeal);
            return dto;
        }

        [HttpDelete]
        [Route("{mealId}")]
        public async Task<ActionResult<MealDto>> DeleteMealAsync(int mealId)
        {
            var selectedMeal = await _mealService.GetMealByIdAsync(mealId);
            if (selectedMeal == null)
                return NotFound();

            await _mealService.DeleteMealAsync(mealId);

            return NoContent();
        }
    }
}