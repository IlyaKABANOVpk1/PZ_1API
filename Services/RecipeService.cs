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
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngridientRepository _ingredientRepository; // Добавили
        private readonly ICategoryRepository _categoryRepository;     // Добавили
        private readonly IMapper _mapper;

        // Обновленный конструктор
        public RecipeService(
            IRecipeRepository recipeRepository,
            IIngridientRepository ingredientRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех рецептов.
        /// </summary>
        public IEnumerable<RecipeDto> GetAll()
        {
            var recipes = _recipeRepository.GetAll();
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        /// <summary>
        /// Получить рецепт по Id.
        /// </summary>
        public RecipeDto? GetById(int id)
        {
            var recipe = _recipeRepository.GetById(id);
            return recipe == null ? null : _mapper.Map<RecipeDto>(recipe);
        }

        /// <summary>
        /// Создать новый рецепт.
        /// </summary>
        public RecipeDto Create(RecipeCreateDto dto)
        {
            // 1. Проверка: Уникальность имени
            if (_recipeRepository.RecipeExists(dto.Name))
            {
                // Лучше использовать кастомные исключения, но пока throw Exception
                throw new Exception($"Рецепт с названием '{dto.Name}' уже существует.");
            }

            // 2. Проверка: Существование категории
            if (!_categoryRepository.Exists(dto.CategoryId))
            {
                throw new Exception($"Категория с ID {dto.CategoryId} не найдена.");
            }

            // 3. Получение ингредиентов из БД
            var ingredients = _ingredientRepository.GetByIds(dto.IngredientIds);

            // Проверка: нашли ли мы все ингредиенты, которые запросил пользователь
            if (ingredients.Count != dto.IngredientIds.Distinct().Count())
            {
                throw new Exception("Один или несколько указанных ингредиентов не существуют.");
            }

            // 4. Маппинг
            // Поскольку у нас сложная связь, надежнее создать объект вручную или настроить AutoMapper.
            // Здесь пример ручного создания для наглядности:
            var recipe = new Recipe
            {
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Ingredients = ingredients // Присваиваем найденные объекты
            };

            // 5. Сохранение
            var created = _recipeRepository.Create(recipe);

            return _mapper.Map<RecipeDto>(created);
        }

        /// <summary>
        /// Обновить рецепт.
        /// </summary>
        public RecipeDto Update(int id, RecipeUpdateDto dto)
        {
            // 1. Получаем существующий рецепт ВМЕСТЕ с ингредиентами
            var existingRecipe = _recipeRepository.GetByIdWithIngredients(id);

            if (existingRecipe == null)
            {
                throw new Exception("Рецепт не найден.");
            }

            // 2. Проверка: Уникальность имени (исключая текущий рецепт)
            // Ищем рецепт с таким же новым именем
            var duplicateCandidate = _recipeRepository.GetByName(dto.Name);

            // Если нашли, и его ID не совпадает с текущим, значит имя занято
            if (duplicateCandidate != null && duplicateCandidate.Id != id)
            {
                throw new Exception($"Имя '{dto.Name}' уже занято другим рецептом.");
            }

            // 3. Проверка категории (если она изменилась)
            if (existingRecipe.CategoryId != dto.CategoryId)
            {
                if (!_categoryRepository.Exists(dto.CategoryId))
                {
                    throw new Exception($"Категория с ID {dto.CategoryId} не найдена.");
                }
            }

            // 4. Обновляем простые поля
            existingRecipe.Name = dto.Name;
            existingRecipe.Description = dto.Description;
            existingRecipe.CategoryId = dto.CategoryId;

            // 5. Обновляем Ингредиенты (Many-to-Many)

            // Получаем новые ингредиенты из БД
            var newIngredients = _ingredientRepository.GetByIds(dto.IngredientIds);

            if (newIngredients.Count != dto.IngredientIds.Distinct().Count())
            {
                throw new Exception("Некорректный список ингредиентов.");
            }

            // Очищаем старый список и добавляем новый
            // Entity Framework отследит изменения в промежуточной таблице
            existingRecipe.Ingredients.Clear();

            foreach (var ingr in newIngredients)
            {
                existingRecipe.Ingredients.Add(ingr);
            }

            // 6. Сохранение
            var updated = _recipeRepository.Update(existingRecipe);

            return _mapper.Map<RecipeDto>(updated);
        }

        /// <summary>
        /// Удалить рецепт.
        /// </summary>
        public bool Delete(int id)
        {
            return _recipeRepository.Delete(id);
        }
    }
}
