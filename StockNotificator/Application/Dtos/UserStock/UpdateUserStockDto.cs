using StockNotificator.Application.Dtos.Base;

namespace StockNotificator.Application.Dtos.UserStock
{
    public record UpdateUserStockDto : BasicBaseDto
    {
        public Guid UserId = Guid.Empty;
        public Guid StockId = Guid.Empty;
        public decimal ReferencePrice = decimal.Zero;
    }
}
