namespace DataStorage.Database.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public required DateTime Timestamp { get; set; }
    }
}
