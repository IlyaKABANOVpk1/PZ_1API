using AutoMapper;
using PZ_1API.Models;
using PZ_1API.Models.DTO;
using PZ_1API.Repositories;

namespace PZ_1API.Services
{
    /// <summary>
    /// Сервис для работы с категориями рецептов.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var categories = _repository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public CategoryDto? GetById(int id)
        {
            var category = _repository.GetById(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public CategoryDto Create(CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            var created = _repository.Create(category);
            return _mapper.Map<CategoryDto>(created);
        }

        public CategoryDto Update(CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            var updated = _repository.Update(category);
            return _mapper.Map<CategoryDto>(updated);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}

