using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PZ_1API.Models.DTO;
using PZ_1API.Services;

namespace PZ_1API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _service;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> GetAll()
        {
            var recipes = _service.GetAll();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeDto> GetById(int id)
        {
            var recipe = _service.GetById(id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpPost]
        public ActionResult<RecipeDto> Create([FromBody] RecipeDto dto)
        {
            var created = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<RecipeDto> Update(int id, [FromBody] RecipeDto dto)
        {
            if (id != dto.Id) return BadRequest("Id mismatch");
            var updated = _service.Update(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
