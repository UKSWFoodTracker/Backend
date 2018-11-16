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
                .ForMember(dto => dto.Name, opt => opt.MapFrom(model => model.Name))
                .ForMember(dto => dto.Ingredients, opt => opt.MapFrom(model => model.MealIngredients));

            CreateMap<Meal, MealUpdateDto>()
                .ReverseMap()
                .ForMember(model => model.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(model => model.MealIngredients, opt => opt.Ignore());

            CreateMap<Meal, MealCreateDto>()
                .ReverseMap()
                .ForMember(model => model.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(model => model.MealIngredients, opt => opt.Ignore());
        }
    }
}
