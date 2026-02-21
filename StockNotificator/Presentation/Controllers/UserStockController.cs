using Microsoft.AspNetCore.Mvc;
using StockNotificator.Application.Dtos.UserStock;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserStockController(IUserStockService userStockService) : ControllerBase
    {
        private readonly IUserStockService _userStockService = userStockService;
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserStockDto request)
        {
            UserStock userStock = new()
            {
                UserId = request.UserId,
                StockId = request.StockId,
                ReferencePrice = request.ReferencePrice,
            };

            var userId = await _userStockService.AddAsync(userStock);
            return CreatedAtAction(nameof(GetById), new { id = userId }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserStockDto request)
        {
            var userStock = await _userStockService.GetByIdAsync(request.Id);
            if (userStock == null)
            {
                return NotFound();
            }

            userStock.UserId = request.UserId;
            userStock.StockId = request.StockId;
            userStock.ReferencePrice = request.ReferencePrice;

            await _userStockService.Update(userStock);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userStockService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userStockService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("exists/{id}")]
        public async Task<IActionResult> Exists(Guid id)
        {
            var exists = await _userStockService.ExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _userStockService.CountAsync();
            return Ok(count);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserStock request)
        {
            await _userStockService.Remove(request);
            return NoContent();
        }
    }
}
