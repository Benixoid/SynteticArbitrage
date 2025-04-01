using System.Globalization;

namespace SharedModels.Kline
{
    //Supported kline intervals (case-sensitive):
    //seconds 1s
    //minutes 1m, 3m, 5m, 15m, 30m
    //hours	1h, 2h, 4h, 6h, 8h, 12h
    //days    1d, 3d
    //weeks	1w
    //months  1M
    public class KlineData
    {
        public ulong OpenTimeLong { get; set; }
        public DateTime OpenTime { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal Volume { get; set; }
        public ulong CloseTimeLong { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal QuoteAssetVolume { get; set; }
        public int NumberOfTrades { get; set; }
        public decimal TakerBuyBaseAssetVolume { get; set; }
        public decimal TakerBuyQuoteAssetVolume { get; set; }
        public int Ignore { get; set; }

        public override string ToString()
        {
            return $"OpenTime: {OpenTime}, ClosePrice: {ClosePrice}";
        }

        public static KlineData ConvertToKlineData(List<object> kline)
        {
            return new KlineData
            {
                OpenTimeLong = Convert.ToUInt64(kline[0]),
                OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(kline[0])).DateTime,
                OpenPrice = decimal.Parse(kline[1].ToString()!, CultureInfo.InvariantCulture),
                HighPrice = decimal.Parse(kline[2].ToString()!, CultureInfo.InvariantCulture),
                LowPrice = decimal.Parse(kline[3].ToString()!, CultureInfo.InvariantCulture),
                ClosePrice = decimal.Parse(kline[4].ToString()!, CultureInfo.InvariantCulture),
                Volume = decimal.Parse(kline[5].ToString()!, CultureInfo.InvariantCulture),
                CloseTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(kline[6])).DateTime,
                CloseTimeLong = Convert.ToUInt64(kline[6]),
                QuoteAssetVolume = decimal.Parse(kline[7].ToString()!, CultureInfo.InvariantCulture),
                NumberOfTrades = Convert.ToInt32(kline[8]),
                TakerBuyBaseAssetVolume = decimal.Parse(kline[9].ToString()!, CultureInfo.InvariantCulture),
                TakerBuyQuoteAssetVolume = decimal.Parse(kline[10].ToString()!, CultureInfo.InvariantCulture),
                Ignore = Convert.ToInt32(kline[11])
            };
        }
    }
}
