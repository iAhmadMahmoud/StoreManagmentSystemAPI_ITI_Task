using System.ComponentModel.DataAnnotations;

namespace Store.Common
{
    public class ProductFilterParameters : BaseFilterParameters
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page Number must be grater than 0")]
        public int MinPrice { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page Number must be grater than 0")]
        public int MaxPrice { get; set; }
    }
}
