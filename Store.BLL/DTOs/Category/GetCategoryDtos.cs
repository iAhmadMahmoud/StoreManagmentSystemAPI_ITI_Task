using System.ComponentModel.DataAnnotations;

namespace Store.BLL
{
    public class GetCategoryDtos
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
