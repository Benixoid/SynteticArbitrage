namespace DataStorage.Database.Entity
{
    public class PriceDifference : BaseEntity
    {        
        public decimal Difference { get; set; }
        public required string Symbol { get; set; }
    }
}
