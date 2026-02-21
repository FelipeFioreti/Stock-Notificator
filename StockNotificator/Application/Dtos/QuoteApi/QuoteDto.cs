namespace StockNotificator.Application.Dtos.BrapiApi
{
    using System.Text.Json.Serialization;

    public record QuoteDto
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonPropertyName("regularMarketPrice")]
        public decimal RegularMarketPrice { get; set; }

        [JsonPropertyName("regularMarketOpen")]
        public decimal RegularMarketOpen { get; set; }

        [JsonPropertyName("regularMarketDayHigh")]
        public decimal RegularMarketDayHigh { get; set; }

        [JsonPropertyName("regularMarketDayLow")]
        public decimal RegularMarketDayLow { get; set; }

        [JsonPropertyName("regularMarketVolume")]
        public long RegularMarketVolume { get; set; }

        [JsonPropertyName("regularMarketTime")]
        public DateTime RegularMarketTime { get; set; }
    }

}
