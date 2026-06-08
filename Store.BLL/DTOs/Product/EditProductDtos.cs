using System.ComponentModel.DataAnnotations;

namespace Store.BLL
{
    public class EditProductDtos
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Count { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }


    }
}
