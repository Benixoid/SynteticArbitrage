
namespace PriceDataFetcher.Service.Background
{
    public class BackgroundPriceReader : BackgroundService
    {
        private readonly IPriceReader _priceReader;
        private readonly IBTCSymbolReader _symbolReader;

        public BackgroundPriceReader(IPriceReader priceReader, IBTCSymbolReader symbolReader)
        {
            _priceReader = priceReader;
            _symbolReader = symbolReader;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var symbol = await _symbolReader.GetSymbolAsync("CURRENT_QUARTER");
            PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while(await timer.WaitForNextTickAsync(stoppingToken))
            {
                // Fetch price data
                Console.WriteLine("Reading price...");
                var result = await _priceReader.GetKlinesAsync(symbol, "1d");
                if (result.IsSuccess)
                {
                    Console.WriteLine("Price read successfully");
                    var price = result.Value?.Last();
                    Console.WriteLine(price);
                }
                else
                {
                    Console.WriteLine("Failed to read price. " + result.ErrorMessage);
                }
            }
        }
    }
}
