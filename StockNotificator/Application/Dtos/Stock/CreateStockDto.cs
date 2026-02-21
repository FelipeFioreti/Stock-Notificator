namespace StockNotificator.Application.Dtos.Stock
{
    public record CreateStockDto
    {
        public string Ticker { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
