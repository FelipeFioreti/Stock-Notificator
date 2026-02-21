using Microsoft.AspNetCore.Mvc;
using StockNotificator.Application.Dtos.User;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto request)
        {
            User user = new()
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = request.PasswordHash,                
            };

            var userId = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = userId }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto request)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null)
            {
                return NotFound();
            }


            user.Email = request.Email;
            user.Name = request.Name;
            user.PasswordHash = request.PasswordHash;
            

            await _userService.Update(user);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("exists/{id}")]
        public async Task<IActionResult> Exists(Guid id)
        {
            var exists = await _userService.ExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _userService.CountAsync();
            return Ok(count);
        }

        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> Find([FromQuery] string email)
        {
            var users = await _userService.FindAsync(u => u.Email == email);
            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] User request)
        {
            await _userService.Remove(request);
            return NoContent();
        }
    }
}
