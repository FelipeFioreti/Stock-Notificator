namespace StockNotificator.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        public void SoftDelete()
        {
            DeletedAt = DateTime.UtcNow;
        }
        public bool IsDeleted()
        {
            return DeletedAt.HasValue;
        }
        public void Restore()
        {
            DeletedAt = null;
        }
    }
}
