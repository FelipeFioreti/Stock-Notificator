using Microsoft.AspNetCore.Mvc;
using StockNotificator.Application.Dtos.StockQuote;
using StockNotificator.Application.Dtos.UserStock;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Application.Services;
using StockNotificator.Domain.Entities;
using System.Runtime.InteropServices;

namespace StockNotificator.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockQuoteController(IStockQuoteService stockQuoteService) : ControllerBase
    {
        private readonly IStockQuoteService _stockQuoteService = stockQuoteService;
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockQuoteDto request)
        {
            StockQuote stockQuote = new()
            {
                Close = request.Close,
                StockId = request.StockId,
                High = request.High,
                Low = request.Low,
                Open = request.Open,
                Volume = request.Volume,
                QuotedAt = request.QuotedAt,
            };

            var userId = await _stockQuoteService.AddAsync(stockQuote);
            return CreatedAtAction(nameof(GetById), new { id = userId }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStockQuoteDto request)
        {
            var stockQuote = await _stockQuoteService.GetByIdAsync(request.Id);
            if (stockQuote == null)
            {
                return NotFound();
            }

            stockQuote.Close = request.Close;
            stockQuote.StockId = request.StockId;
            stockQuote.High = request.High;
            stockQuote.Low = request.Low;
            stockQuote.Open = request.Open;
            stockQuote.Volume = request.Volume;
            stockQuote.QuotedAt = request.QuotedAt;

            await _stockQuoteService.Update(stockQuote);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _stockQuoteService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _stockQuoteService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("exists/{id}")]
        public async Task<IActionResult> Exists(Guid id)
        {
            var exists = await _stockQuoteService.ExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _stockQuoteService.CountAsync();
            return Ok(count);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] StockQuote request)
        {
            await _stockQuoteService.Remove(request);
            return NoContent();
        }
    }
}
