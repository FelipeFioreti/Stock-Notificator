using System.ComponentModel.DataAnnotations;

namespace StockNotificator.Domain.Entities
{
    public class StockQuote : BaseEntity
    {
        [Required]
        public Guid StockId { get; set; }

        [Required]
        public DateTime QuotedAt { get; set; }

        [Required]
        public decimal Open { get; set; }

        [Required]
        public decimal High { get; set; }

        [Required]
        public decimal Low { get; set; }

        [Required]
        public decimal Close { get; set; }

        public long Volume { get; set; }

        public Stock Stock { get; set; } = null!;
    }
}
