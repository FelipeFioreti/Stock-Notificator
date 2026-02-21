using StockNotificator.Application.Dtos.Base;

namespace StockNotificator.Application.Dtos.Stock
{
    public record UpdateStockDto : BasicBaseDto
    {
        public string Ticker { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
