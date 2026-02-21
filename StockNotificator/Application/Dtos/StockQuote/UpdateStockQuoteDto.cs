using StockNotificator.Application.Dtos.Base;

namespace StockNotificator.Application.Dtos.StockQuote
{
    public record UpdateStockQuoteDto : BasicBaseDto
    {
        public Guid StockId { get; set; } = Guid.Empty;
        public DateTime QuotedAt { get; set; } = DateTime.Now;
        public decimal Open { get; set; } = decimal.Zero;
        public decimal High { get; set; } = decimal.Zero;
        public decimal Low { get; set; } = decimal.Zero;
        public decimal Close { get; set; } = decimal.Zero;
        public long Volume { get; set; } = 0;
    }
}
