namespace PriceDataFetcher.Service
{
    public interface IBTCSymbolReader
    {
        Task<string> GetSymbolAsync(string quarter);        
    }
}
