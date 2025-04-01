using PriceDataFetcher.Models.ExchangeInfo;

namespace PriceDataFetcher.Service
{
    public interface ISymbolInfoReader
    {
        Task<ExchangeRoot?> ReadData();
    }
}
