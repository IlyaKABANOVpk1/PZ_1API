using AutoMapper;
using PZ_1API.Models;
using PZ_1API.Models.DTO;
using PZ_1API.Repositories;

namespace PZ_1API.Services
{
    /// <summary>
    /// Сервис для работы с категориями рецептов.
    /// </summary>
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех категорий.
        /// </summary>
        public IEnumerable<CategoryDto> GetAll()
        {
            var categories = _repository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        /// <summary>
        /// Получить категорию по Id.
        /// </summary>
        public CategoryDto? GetById(int id)
        {
            var category = _repository.GetById(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        /// <summary>
        /// Создать новую категорию.
        /// </summary>
        public CategoryDto Create(CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            var created = _repository.Create(category);
            return _mapper.Map<CategoryDto>(created);
        }

        /// <summary>
        /// Обновить категорию.
        /// </summary>
        public CategoryDto Update(CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            var updated = _repository.Update(category);
            return _mapper.Map<CategoryDto>(updated);
        }

        /// <summary>
        /// Удалить категорию по Id.
        /// </summary>
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
