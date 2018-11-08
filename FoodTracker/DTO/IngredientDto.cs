using System.ComponentModel.DataAnnotations;

namespace FoodTracker.DTO
{
    public class IngredientDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Calories { get; set; }
    }
}
