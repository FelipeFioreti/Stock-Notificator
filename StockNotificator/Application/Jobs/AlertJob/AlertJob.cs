using Quartz;
using StockNotificator.Application.Dtos.BrapiApi;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Application.Providers;
using StockNotificator.Domain.Entities;
using System.Text.Json;

namespace StockNotificator.Application.Jobs.AlertJob
{
    public class AlertJob(
        ILogger<AlertJob> logger,
        IStockService stockService,
        IStockQuoteService stockQuoteService,
        IAlertConditionService alertConditionService,
        QuoteProvider quoteProvider,
        INotificationService notificationService) : IJob
    {
        private readonly ILogger<AlertJob> _logger = logger;
        private readonly IStockService _stockService = stockService;
        private readonly IStockQuoteService _stockQuoteService = stockQuoteService;
        private readonly IAlertConditionService _alertConditionService = alertConditionService;
        private readonly QuoteProvider _quoteProvider = quoteProvider;
        private readonly INotificationService _notificationService = notificationService;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("AlertJob starting...");

            IEnumerable<Stock> stocks = await _stockService.GetByUsers();

            foreach (Stock stock in stocks)
            {
                _logger.LogInformation($"Processing stock: {stock.Ticker} - {stock.Name}");

                StockQuote? stockQuote = await _quoteProvider.GetQuoteAsync(stock.Ticker);

                if (stockQuote != null)
                {
                    stockQuote.StockId = stock.Id;

                    _logger.LogInformation($"Retrieved quote for {stock.Ticker}");

                    var alerts = await _alertConditionService.GetByStock(stock.Id);
                        
                    alerts.ToList()
                        .ForEach(alert =>
                        {
                            if (alert.VerifyCondition(stockQuote))
                            {
                                _logger.LogInformation($"Alert condition met for {stock.Ticker} - Alert ID: {alert.Id}");

                                _notificationService.SendEmailAsync(alert.UserStock.User.Email, "Notificação de ação", $"Alerta ativo para ação: ${stock.Ticker}, ação chegou ao valor de: ${stockQuote.Close} ");

                                return;
                            }
                        });

                    continue;
                }
                    
                _logger.LogWarning($"Failed to retrieve quote for {stock.Ticker}");
            }

            return;
        }
    }
}
