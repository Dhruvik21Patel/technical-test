using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities.DataModels
{

    public class Category : AuditableEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }

}