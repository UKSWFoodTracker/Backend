using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTracker.DTO
{
    public class MealCreateDto
    {
        [Required]
        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
