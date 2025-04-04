﻿namespace PriceDataFetcher.Models.ExchangeInfo
{
    
    public class Filter
    {
        public string? minPrice { get; set; }
        public string? maxPrice { get; set; }
        public string? filterType { get; set; }
        public string? tickSize { get; set; }
        public string? stepSize { get; set; }
        public string? maxQty { get; set; }
        public string? minQty { get; set; }
        public int? limit { get; set; }
        public string? multiplierDown { get; set; }
        public string? multiplierUp { get; set; }
        public string? multiplierDecimal { get; set; }
    }


}
