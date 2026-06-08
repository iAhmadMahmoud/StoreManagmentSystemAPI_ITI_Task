
namespace Store.DAL
{
    public class Category:IAuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products{ get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set; }
    }
}
