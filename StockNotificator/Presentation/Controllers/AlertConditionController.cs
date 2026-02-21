using Microsoft.AspNetCore.Mvc;
using StockNotificator.Application.Dtos.AlertCondition;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertConditionController(IAlertConditionService alertConditionService) : ControllerBase
    {
        private readonly IAlertConditionService _alertConditionService = alertConditionService;
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAlertConditionDto request)
        {
            AlertCondition alertCondition = new()
            {
                Operator = request.Operator,
                TargetValue = request.TargetValue,
                Type = request.Type,
                UserStockId = request.UserStockId,
            };

            var userId = await _alertConditionService.AddAsync(alertCondition);
            return CreatedAtAction(nameof(GetById), new { id = userId }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAlertConditionDto request)
        {
            var alertCondition = await _alertConditionService.GetByIdAsync(request.Id);
            if (alertCondition == null)
            {
                return NotFound();
            }

            alertCondition.Operator = request.Operator;
            alertCondition.TargetValue = request.TargetValue;
            alertCondition.Type = request.Type;
            alertCondition.UserStockId = request.UserStockId;

            await _alertConditionService.Update(alertCondition);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _alertConditionService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _alertConditionService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("exists/{id}")]
        public async Task<IActionResult> Exists(Guid id)
        {
            var exists = await _alertConditionService.ExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _alertConditionService.CountAsync();
            return Ok(count);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] AlertCondition request)
        {
            await _alertConditionService.Remove(request);
            return NoContent();
        }
    }
}
