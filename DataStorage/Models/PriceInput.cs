namespace DataStorage.Models
{
    public class PriceInput
    {
        public decimal CurrentPrice { get; set; }
        public required string Symbol { get; set; }        
        public required DateTime PriceDate { get; set; }
    }
}
