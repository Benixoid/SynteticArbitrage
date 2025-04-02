using Newtonsoft.Json;
using PriceDataFetcher.Models.ExchangeInfo;
using SharedModels.Kline;
using SharedModels.Response;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TryToRead
{
    
    internal class Program
    {
        public static async Task Main(string[] args)
        {            
            Console.WriteLine("Enter following:");
            Console.WriteLine("'last1' - to read last 10 values from BINANCE and write to DB (BTCUSDT_QUARTER)");
            Console.WriteLine("'last2' - to read last 10 values from BINANCE and write to DB (BTCUSDT_BI-QUARTER)");
            Console.WriteLine("'exit' - to exit from application");
            string? enteredValue;
            do
            {
                Console.Write("Enter command: ");
                enteredValue = Console.ReadLine();
                if (enteredValue!.ToUpper().Equals("LAST1"))
                {
                    Console.WriteLine("Reading last 10 values from BINANCE (BTCUSDT_QUARTER) and writing to DB...");
                    await ReadLast10QuarterValuesFromBinanceAndWriteToDB();
                } else if (enteredValue!.ToUpper().Equals("LAST2"))
                {
                    Console.WriteLine("Reading last 10 values from BINANCE (BTCUSDT_BI-QUARTER) and writing to DB...");
                    await ReadLast10BiQuarterValuesFromBinanceAndWriteToDB();
                }
            }
            while (!enteredValue!.ToUpper().Equals("EXIT"));            
            //string url = "https://dapi.binance.com/dapi/v1/klines?symbol=BTCUSD&interval=1d";
            //string url = "https://dapi.binance.com/dapi/v1/klines?symbol=BTCUSD_250627&interval=1d";
            //string url = "https://dapi.binance.com/dapi/v1/klines?symbol=BTCUSD_250926&interval=1h";            
        }
        private static async Task ReadLast10BiQuarterValuesFromBinanceAndWriteToDB()
        {
            string? symbol_biquarter;

            //string url_biquarter = "https://localhost:7257/api/v1/Info/btcusdt_bi_quarter_symbol";
            string url_biquarter = "https://localhost:5001/gateway/Info/btcusdt_bi_quarter_symbol";

            //string baseurl_data = "https://localhost:7257/api/v1/last10prices/";
            string baseurl_data = "https://localhost:5001/gateway/last10prices/";

            
            symbol_biquarter = await GetSymbol(url_biquarter, "BiQuarter");
            if (symbol_biquarter is null)
                return;

            string url_data = $"{baseurl_data}{symbol_biquarter}?interval=1h";

            var list = await GetData(url_data);
            if (list is null)
                return;

            foreach (var item in list)
            {
                ApiResponse<bool> result = await SaveData(item, symbol_biquarter);
                if (!result.Success)
                    Console.WriteLine("failed! " + result.Message);
            }
        }

        private static async Task ReadLast10QuarterValuesFromBinanceAndWriteToDB()
        {
            string? symbol_quarter;            
            //string url_quarter = "https://localhost:7257/api/v1/Info/btcusdt_quarter_symbol";            
            //string baseurl_data = "https://localhost:7257/api/v1/last10prices/";
            
            string url_quarter = "https://localhost:5001/gateway/Info/btcusdt_quarter_symbol";
            string baseurl_data = "https://localhost:5001/gateway/last10prices/";
            symbol_quarter = await GetSymbol(url_quarter, "Quarter");
            if (symbol_quarter is null)                            
                return;           

            string url_data = $"{baseurl_data}{symbol_quarter}?interval=1h";
            
            var list = await GetData(url_data);
            if (list is null)                
                return;            
            
            foreach (var item in list)
            {                
                ApiResponse<bool> result = await SaveData(item, symbol_quarter);
                if (!result.Success)
                    Console.WriteLine("failed! " + result.Message);                    
            }
        }

        private static async Task<ApiResponse<bool>> SaveData(KlineData item, string symbol)
        {
            Console.Write("Saving price to DB. " + item + "...");
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:7084/");
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var data = new { CurrentPrice = item.ClosePrice, Symbol = symbol, PriceDate = item.CloseTime };
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                try
                {                    
                    HttpResponseMessage response = await client.PostAsync("gateway/Price", content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponseData = await response.Content.ReadAsStringAsync();
                        if (apiResponseData is null)                                                    
                            return ApiResponse<bool>.ErrorResponse("Failed to get data from api: " + response.RequestMessage!.ToString());
                        
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(apiResponseData);
                        if (apiResponse == null)
                            return ApiResponse<bool>.ErrorResponse("Response is null");
                        if (!apiResponse.Success)
                            return ApiResponse<bool>.ErrorResponse("Failed to save data to DB. Message: " + apiResponse.Message);
                        
                        Console.WriteLine("done!");
                        return ApiResponse<bool>.SuccessResponse(true);                        
                    }
                    else
                    {
                        return ApiResponse<bool>.ErrorResponse("Failed to save data to DB. Status code: " + response.StatusCode);                        
                    }
                }
                catch (Exception ex)
                {
                    return ApiResponse<bool>.ErrorResponse("Exception during data saving. Status code: " + ex.Message);                    
                }
            }
                
        }

       

        private static async Task<List<KlineData>?> GetData(string url)
        {
            Console.Write("Getting data...");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Failed to get data from api ({url}). Status code: {response.StatusCode}");
                        return null;
                    }
                    var data = await response.Content.ReadAsStringAsync();
                    if (data is null)
                    {
                        Console.WriteLine("Failed to get data from api: " + response.RequestMessage!.ToString());
                        return null;
                    }
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<KlineData>>>(data);
                    if (apiResponse != null && apiResponse.Success)
                    {
                        Console.WriteLine("done!");
                        return apiResponse.Data!;
                    }
                    Console.WriteLine("No data for Quarter values!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in getting data: " + ex.Message);
                return null;
            }
        }
        private static async Task<string?> GetSymbol(string url_quarter, string title)
        {
            Console.Write("Getting Quarter symbol...");
            try
            {
                using (HttpClient client = new HttpClient())
                {                    
                    var response = await client.GetStringAsync(url_quarter);
                    if (response is not null)
                    {
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(response);
                        if (apiResponse != null && apiResponse.Success)
                        {
                            Console.WriteLine(apiResponse.Data!);
                            return apiResponse.Data!;
                        }                            
                        else                        
                            Console.WriteLine("Failed to get quarter symbol data: " + apiResponse!.Message);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in getting quarter symbol data: " + ex.Message);
                return null;
            }
        }
    }
}
