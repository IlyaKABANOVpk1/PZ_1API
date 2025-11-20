using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public RecipeController(IRecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<RecipeDto>> GetAll()
        {
            var recipes = _service.GetAll();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<RecipeDto> GetById(int id)
        {
            var recipe = _service.GetById(id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpPost]
        public ActionResult<RecipeDto> Create([FromBody] RecipeCreateDto dto)
        {
            try
            {
                var created = _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Админ")]
        [HttpPut("{id}")]
        public ActionResult<RecipeDto> Update(int id, [FromBody] RecipeUpdateDto dto)
        {
            try
            {
                var updated = _service.Update(id, dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Админ")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
