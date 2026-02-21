namespace StockNotificator.Application.Dtos.UserStock
{
    public record CreateUserStockDto
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid StockId { get; set; } = Guid.Empty;
        public decimal ReferencePrice { get; set; } = decimal.Zero;
    }
}
