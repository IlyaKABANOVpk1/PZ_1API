using AutoMapper;
using PZ_1API.Models;
using PZ_1API.Models.DTO;
using PZ_1API.Repositories;

namespace PZ_1API.Services
{
    /// <summary>
    /// Сервис для работы с рецептами.
    /// </summary>
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех рецептов.
        /// </summary>
        public IEnumerable<RecipeDto> GetAll()
        {
            var recipes = _repository.GetAll();
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        /// <summary>
        /// Получить рецепт по Id.
        /// </summary>
        public RecipeDto? GetById(int id)
        {
            var recipe = _repository.GetById(id);
            return recipe == null ? null : _mapper.Map<RecipeDto>(recipe);
        }

        /// <summary>
        /// Создать новый рецепт.
        /// </summary>
        public RecipeDto Create(RecipeDto dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);
            var created = _repository.Create(recipe);
            return _mapper.Map<RecipeDto>(created);
        }

        /// <summary>
        /// Обновить рецепт.
        /// </summary>
        public RecipeDto Update(RecipeDto dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);
            var updated = _repository.Update(recipe);
            return _mapper.Map<RecipeDto>(updated);
        }

        /// <summary>
        /// Удалить рецепт.
        /// </summary>
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
