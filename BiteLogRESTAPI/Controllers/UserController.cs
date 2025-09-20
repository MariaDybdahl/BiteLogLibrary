using BiteLogLibrary.DTO;
using BiteLogLibrary.Interface.Repository;
using BiteLogLibrary.Interface.Services;
using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using BiteLogLibrary.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiteLogRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<User>>> Get()
        {
            var user = await _userRepository.GetAllAsync();
            return Ok(user);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> Post([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                User createdUser = await _userService.RegisterAsync(registerRequest);
                return Created(
                    Url.ActionContext.HttpContext.Request.Path + "/" + createdUser.Id,
                    createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
        {
            User? updatedUser = await _userRepository.UpdateAsync(id, user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(updatedUser);
            }

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User? deletedUser = await _userRepository.DeleteAsync(id);
            if (deletedUser == null) { return NotFound("No such book, id: " + id); }
            return Ok(deletedUser);
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var result = await _userService.AuthenticateAsync(req);
            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            // match din frontend-kontrakt
            return Ok(new
            {
                success = true,
                message = result.Message,
                user = new { result.User!.Id, result.User!.Username, result.User!.Email }
                // token = result.Token // hvis/naar du tilføjer JWT
            });

        }
    } 
}
