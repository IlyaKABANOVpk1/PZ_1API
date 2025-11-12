using Microsoft.AspNetCore.Mvc;
using PZ_1API.Models.DTO;
using PZ_1API.Services;

namespace PZ_1API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<IngredientDto>> GetAll()
        {
            var ingredients = _service.GetAll();
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        public ActionResult<IngredientDto> GetById(int id)
        {
            var ingredient = _service.GetById(id);
            if (ingredient == null) return NotFound();
            return Ok(ingredient);
        }

        [HttpPost]
        public ActionResult<IngredientDto> Create([FromBody] IngredientDto dto)
        {
            var created = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<IngredientDto> Update(int id, [FromBody] IngredientDto dto)
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
