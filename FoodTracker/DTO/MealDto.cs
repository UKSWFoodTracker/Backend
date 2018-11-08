using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTracker.DTO
{
    public class MealDto
    {
        public int Id { get; set; }
        [Required]
        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
