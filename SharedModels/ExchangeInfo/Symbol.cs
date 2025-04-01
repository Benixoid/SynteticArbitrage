namespace PriceDataFetcher.Models.ExchangeInfo
{
    public class Symbol
    {
        public string? symbol { get; set; }
        public string? pair { get; set; }
        public string? contractType { get; set; }
        public required object deliveryDate { get; set; }
        public required object onboardDate { get; set; }
        public string? contractStatus { get; set; }
        public int contractSize { get; set; }
        public string? marginAsset { get; set; }
        public string? maintMarginPercent { get; set; }
        public string? requiredMarginPercent { get; set; }
        public string? baseAsset { get; set; }
        public string? quoteAsset { get; set; }
        public int pricePrecision { get; set; }
        public int quantityPrecision { get; set; }
        public int baseAssetPrecision { get; set; }
        public int quotePrecision { get; set; }
        public int equalQtyPrecision { get; set; }
        public int maxMoveOrderLimit { get; set; }
        public string? triggerProtect { get; set; }
        public string? underlyingType { get; set; }
        public List<string>? underlyingSubType { get; set; }
        public List<Filter>? filters { get; set; }
        public List<string>? orderTypes { get; set; }
        public List<string>? timeInForce { get; set; }
        public string? liquidationFee { get; set; }
        public string? marketTakeBound { get; set; }
    }


}
