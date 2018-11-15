using AutoMapper;
using FoodTracker.Model;

namespace FoodTracker.DTO.Mapping
{
    public class DtosProfile : Profile
    {
        public DtosProfile()
        {
            CreateMap<Ingredient, IngredientDto>()
                .ReverseMap()
                .ForMember(model => model.MealIngredients, opt => opt.Ignore());

            CreateMap<MealIngredient, IngredientDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => model.Ingredient.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(model => model.Ingredient.Name))
                .ForMember(dto => dto.Calories, opt => opt.MapFrom(model => model.Ingredient.Calories))
                .ReverseMap()
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<Meal, MealDto>()
                .ForMember(dto => dto.Ingredients,
                    option => option.MapFrom(model => model.MealIngredients))
                .ReverseMap()
                .ForMember(model => model.MealIngredients, opt => opt.Ignore());

            CreateMap<Meal, MealUpdateDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => model.Id))
                .ForMember(dto => dto.Ingredients, opt => opt.MapFrom(model => model.MealIngredients))
                .ReverseMap()
                .ForMember(model => model.MealIngredients, opt => opt.Ignore());
        }
    }
}
