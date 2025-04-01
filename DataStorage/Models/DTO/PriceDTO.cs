namespace DataStorage.Models.DTO
{
    public class PriceDTO : BaseDTO
    {
        public decimal CurrentPrice { get; set; }
        public required string Symbol { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
