using System;
using System.Collections.Generic;

namespace FoodTracker.DTO
{
    public class MealDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IngredientDto> Ingredients { get; set; }
        public DateTime Created { get; set; }
    }
}
