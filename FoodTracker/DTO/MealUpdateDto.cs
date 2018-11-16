using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTracker.DTO
{
    public class MealUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
