using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiteLogRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomMealFoodController : ControllerBase
    {
        private readonly CustomMealFoodRepository _customMealFoodRepository;

        public CustomMealFoodController(CustomMealFoodRepository customMealFoodRepository)
        {
            _customMealFoodRepository = customMealFoodRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomMealFood>>> Get()
        {
            var customMealFoods = await _customMealFoodRepository.GetAllAsync();
            return Ok(customMealFoods);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var customMealFood = await _customMealFoodRepository.GetByIdAsync(id);
            return customMealFood == null ? NotFound() : Ok(customMealFood);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomMealFood>> Post([FromBody] CustomMealFood customMealFood)
        {

            CustomMealFood createdCustomMealFood = await _customMealFoodRepository.AddAsync(customMealFood);
            return Created(
                Url.ActionContext.HttpContext.Request.Path + "/" + createdCustomMealFood.Id,
                createdCustomMealFood);
        }
    }
}
