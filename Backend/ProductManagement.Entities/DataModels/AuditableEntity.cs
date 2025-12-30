using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Entities.DataModels
{
    public abstract class AuditableEntity
    {
        [ForeignKey("CreatedByUser")]
        public int? CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("UpdatedByUser")]
        public int? UpdatedBy { get; set; }
        public User? UpdatedByUser { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey("DeletedByUser")]
        public int? DeletedBy { get; set; }
        public User? DeletedByUser { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}