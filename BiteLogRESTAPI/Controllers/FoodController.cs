using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiteLogRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private FoodRepository _foodRepository;

        public FoodController(FoodRepository FoodRepository)
        {
            _foodRepository = FoodRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Food>>> Get()
        {
            var food = await _foodRepository.GetAllAsync();
            return Ok(food);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            return food == null ? NotFound() : Ok(food);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Food>> Post([FromBody] Food food)
        {
            
            Food createdFood = await _foodRepository.AddAsync(food);
            return Created(
                Url.ActionContext.HttpContext.Request.Path + "/" + createdFood.Id,
                createdFood);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Food>> Put(int id, [FromBody] Food food)
        {
            Food? updatedFood = await _foodRepository.UpdateAsync(id, food);
            if (updatedFood == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(updatedFood);
            }

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Food>> Delete(int id)
        {
            Food? deletedFood = await _foodRepository.DeleteAsync(id);
            if (deletedFood == null) { return NotFound("No such book, id: " + id); }
            return Ok(deletedFood);
        }
    }
}
