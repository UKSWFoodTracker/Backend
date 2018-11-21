using System.Collections.Generic;
using System.Linq;
using FoodTracker.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.Model;
using Microsoft.AspNetCore.Authorization;

namespace FoodTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;
        private readonly IMapper _mapper;

        public IngredientsController(IIngredientsService ingredientsService, IMapper mapper)
        {
            _ingredientsService = ingredientsService;
            _mapper = mapper;
        }

        [HttpGet, Authorize]
        public IEnumerable<IngredientDto> GetAllIngredients()
        {
            var ingredients = _ingredientsService.GetAllIngredients();
            var dtos = ingredients.Select(i => _mapper.Map<Ingredient, IngredientDto>(i));

            return dtos;
        }
    }
}