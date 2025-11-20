using AutoMapper;
using PZ_1API.Models.DTO;

namespace PZ_1API.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients));

            CreateMap<RecipeCreateDto, Recipe>()
                .ForMember(dest => dest.Ingredients, opt => opt.Ignore());

            CreateMap<RecipeUpdateDto, Recipe>()
                .ForMember(dest => dest.Ingredients, opt => opt.Ignore());

            CreateMap<User, AuthResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
