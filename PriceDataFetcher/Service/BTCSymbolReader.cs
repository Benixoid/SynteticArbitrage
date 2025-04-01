using PriceDataFetcher.Controllers;

namespace PriceDataFetcher.Service
{
    public class BTCSymbolReader : IBTCSymbolReader
    {
        private readonly ILogger<InfoController> _logger;
        private readonly ISymbolInfoReader _symbolInfoReader;
        public BTCSymbolReader(ILogger<InfoController> logger, ISymbolInfoReader symbolInfoReader)
        {
            _logger = logger;
            _symbolInfoReader = symbolInfoReader;
        }

        public async Task<string> GetSymbolAsync(string quarter)
        {
            _logger.LogInformation("GetSymbolQuarterAsync called");
            var info = await _symbolInfoReader.ReadData();
            if (info is null)
                return "";
            
            var quarterSymbolInfo = info.symbols?.FirstOrDefault(s => s.pair!.Equals("BTCUSD") && s.contractType!.Equals(quarter));
            if (quarterSymbolInfo is null)            
                return "";
            
            return quarterSymbolInfo.symbol!;        
        }
    }
}
