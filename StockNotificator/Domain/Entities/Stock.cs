using System.ComponentModel.DataAnnotations;

namespace StockNotificator.Domain.Entities
{
    public class Stock : BaseEntity
    {
        [Required(ErrorMessage = "Ticker is required.")]
        [StringLength(5)]
        public string Ticker { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ticker is required.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 character.")]
        public string Name { get; set; } = string.Empty;

        public ICollection<UserStock> UserStocks { get; set; } = [];
        public ICollection<StockQuote> StockQuotes { get; set; } = [];
    }
}
