using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Store.BLL
{
    public class CreateProductDtos
    {
        
        public string Title { get; set; }=string.Empty;

        public string Description { get; set; }= string.Empty;

        public decimal Price { get; set; }

        public int Count { get; set; }
       
        public DateTime? ExpiryDate { get; set; }

        public int CategoryId { get; set; }


    }
}
