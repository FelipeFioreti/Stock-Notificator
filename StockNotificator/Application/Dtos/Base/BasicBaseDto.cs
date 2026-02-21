namespace StockNotificator.Application.Dtos.Base
{
    public record BasicBaseDto
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}
