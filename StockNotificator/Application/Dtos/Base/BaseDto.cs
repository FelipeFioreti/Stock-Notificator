namespace StockNotificator.Application.Dtos.Base
{
    public record BaseDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
    }
}
