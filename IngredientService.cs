using AutoMapper;
using PZ_1API.Models;
using PZ_1API.Models.DTO;
using PZ_1API.Repositories;

namespace PZ_1API.Services
{
    /// <summary>
    /// Сервис для работы с ингредиентами.
    /// </summary>
    public class IngredientService
    {
        private readonly IIngridientRepository _repository;
        private readonly IMapper _mapper;

        public IngredientService(IIngridientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех ингредиентов.
        /// </summary>
        public IEnumerable<IngredientDto> GetAll()
        {
            var ingredients = _repository.GetAll();
            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }

        /// <summary>
        /// Получить ингредиент по Id.
        /// </summary>
        public IngredientDto? GetById(int id)
        {
            var ingredient = _repository.GetById(id);
            return ingredient == null ? null : _mapper.Map<IngredientDto>(ingredient);
        }

        /// <summary>
        /// Создать новый ингредиент.
        /// </summary>
        public IngredientDto Create(IngredientDto dto)
        {
            var ingredient = _mapper.Map<Ingredient>(dto);
            var created = _repository.Create(ingredient);
            return _mapper.Map<IngredientDto>(created);
        }

        /// <summary>
        /// Обновить ингредиент.
        /// </summary>
        public IngredientDto Update(IngredientDto dto)
        {
            var ingredient = _mapper.Map<Ingredient>(dto);
            var updated = _repository.Update(ingredient);
            return _mapper.Map<IngredientDto>(updated);
        }

        /// <summary>
        /// Удалить ингредиент.
        /// </summary>
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
