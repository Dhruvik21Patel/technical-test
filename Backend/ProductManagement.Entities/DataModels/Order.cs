namespace ProductManagement.Entities.DataModels
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }   // ← CustomerId = UserId
        public User Customer { get; set; }
        public decimal OrderValue { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }
    }
}