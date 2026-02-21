
using StockNotificator.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockNotificator.Domain.Entities
{
    public class AlertCondition : BaseEntity
    {
        [Required]
        [ForeignKey("UserStocks")]
        public Guid UserStockId { get; set; }

        [Required]
        [EnumDataType(typeof(ConditionsType))]
        public ConditionsType Type { get; set; }

        [Required]
        [EnumDataType(typeof(ConditionOperator))]
        public ConditionOperator Operator { get; set; }

        [Required]
        public decimal TargetValue { get; set; }
        public virtual UserStock UserStock { get; set; } = null!;

        public bool VerifyCondition(StockQuote stockQuote)
        {
            decimal referenceValue = UserStock.ReferencePrice;
            decimal currentValue = stockQuote.Close;

            switch (Type)
            {
                case ConditionsType.Porcentage:
                    decimal percentageChange = ((currentValue - referenceValue) / referenceValue) * 100;
                    return Compare(percentageChange, TargetValue);

                case ConditionsType.AbsolutePrice:
                    return Compare(currentValue, TargetValue);

                case ConditionsType.DailyVariation:
                    decimal dailyVariation = currentValue - stockQuote.Open;
                    return Compare(dailyVariation, TargetValue);

                default:
                    return false;
            }
        }

        private bool Compare(decimal left, decimal right)
        {
            return Operator switch
            {
                ConditionOperator.GreaterThan => left > right,
                ConditionOperator.GreaterOrEqual => left >= right,
                ConditionOperator.LessThan => left < right,
                ConditionOperator.LessOrEqual => left <= right,
                ConditionOperator.Equal => left == right,
                _ => false
            };
        }
    }
}
