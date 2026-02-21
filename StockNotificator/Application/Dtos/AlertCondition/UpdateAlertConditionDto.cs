using StockNotificator.Application.Dtos.Base;
using StockNotificator.Domain.Enums;

namespace StockNotificator.Application.Dtos.AlertCondition
{
    public record UpdateAlertConditionDto : BasicBaseDto
    {
        public Guid UserStockId { get; set; } = Guid.Empty;
        public ConditionsType Type { get; set; } = ConditionsType.Undefined;
        public ConditionOperator Operator { get; set; } = ConditionOperator.Undefined;
        public decimal TargetValue { get; set; } = decimal.Zero;
    }
}
