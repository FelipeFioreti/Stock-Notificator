using StockNotificator.Application.Dtos.BrapiApi;
using StockNotificator.Application.Interfaces.Providers;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Providers
{
    public class QuoteProvider(IHttpClientFactory httpClientFactory, ILogger<QuoteProvider> logger, IConfiguration configuration) : IQuoteProvider
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<QuoteProvider> _logger = logger;

        public async Task<StockQuote?> GetQuoteAsync(string ticker)
        {
            try
            {
                string token = _configuration["BRAPI_API_KEY"]!;

                HttpClient client = _httpClientFactory.CreateClient("Brapi");

                HttpResponseMessage response = await client.GetAsync($"api/quote/{ticker}?token={token}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning(
                        "Falha ao consultar cotação {Ticker}. StatusCode: {StatusCode}",
                        ticker,
                        response.StatusCode);

                    return null;
                }

                QuoteResponseDto? dto = await response.Content
                    .ReadFromJsonAsync<QuoteResponseDto>();

                var quote = dto?.Results.FirstOrDefault();
                if (quote is null)
                    return null;

                return new StockQuote
                {
                    QuotedAt = quote.RegularMarketTime,
                    Open = quote.RegularMarketOpen,
                    High = quote.RegularMarketDayHigh,
                    Low = quote.RegularMarketDayLow,
                    Close = quote.RegularMarketPrice,
                    Volume = quote.RegularMarketVolume
                };
            } catch (Exception ex) {

                _logger.LogWarning(
                        "Erro ao consultar cotação para {ticker}. Erro: {ex}",
                        ticker, ex);

                return null;
            }
        }

        private sealed record QuoteResponseDto
        {
            public IReadOnlyList<QuoteDto> Results { get; init; } = [];
        }
    }
}
