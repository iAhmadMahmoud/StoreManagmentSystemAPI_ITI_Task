namespace Store.DAL
{
    public class Product:IAuditableEntity
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int  Count { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime? ExpriryDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
