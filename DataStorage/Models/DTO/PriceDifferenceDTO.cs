namespace DataStorage.Models.DTO
{
    public class PriceDifferenceDTO : BaseDTO
    {
        public decimal Difference { get; set; }
        public required string Symbol { get; set; }
    }
}
