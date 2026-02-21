using StockNotificator.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StockNotificator.Application.Dtos.AlertCondition
{
    public record CreateAlertConditionDto
    {
        [Required]
        public Guid UserStockId { get; set; }
        [Required]
        public ConditionsType Type { get; set; }
        [Required]
        public ConditionOperator Operator { get; set; }
        [Required]
        public decimal TargetValue { get; set; }
    }
}
