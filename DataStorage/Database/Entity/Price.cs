namespace DataStorage.Database.Entity
{
    public class Price : BaseEntity
    {
        public decimal CurrentPrice { get; set; }
        public required string Symbol { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
