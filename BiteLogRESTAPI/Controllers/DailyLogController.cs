using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiteLogRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyLogController : ControllerBase
    {
        private readonly DailyLogRepository _dailyLogRepository;

        public DailyLogController(DailyLogRepository dailyLogRepository)
        {
            _dailyLogRepository = dailyLogRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DailyLog>>> Get()
        {
            var dailyLog = await _dailyLogRepository.GetAllAsync();
            return Ok(dailyLog);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var dailylog = await _dailyLogRepository.GetByIdAsync(id);
            return dailylog == null ? NotFound() : Ok(dailylog);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomMealFood>> Post([FromBody] DailyLog dailyLog)
        {

            DailyLog createdDailyLog = await _dailyLogRepository.AddAsync(dailyLog);
            return Created(
                Url.ActionContext.HttpContext.Request.Path + "/" + createdDailyLog.Id,
                createdDailyLog);
        }
    }
}
