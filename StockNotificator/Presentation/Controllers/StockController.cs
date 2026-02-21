using Microsoft.AspNetCore.Mvc;
using StockNotificator.Application.Dtos.Stock;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController(IStockService stockService) : ControllerBase
    {
        private readonly IStockService _stockService = stockService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto request)
        {
            Stock stock = new()
            {
                Ticker = request.Ticker,
                Name = request.Name,
            };

            var stockId = await _stockService.AddAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stockId }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStockDto request)
        {
            var stock = await _stockService.GetByIdAsync(request.Id);
            if (stock == null)
            {
                return NotFound();
            }

            stock.Ticker = request.Ticker;
            stock.Name = request.Name;

            await _stockService.Update(stock);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _stockService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _stockService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("exists/{id}")]
        public async Task<IActionResult> Exists(Guid id)
        {
            var exists = await _stockService.ExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _stockService.CountAsync();
            return Ok(count);
        }

        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> Find([FromQuery] string ticker)
        {
            var users = await _stockService.FindAsync(u => u.Ticker == ticker);
            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Stock request)
        {
            await _stockService.Remove(request);
            return NoContent();
        }
    }
}
