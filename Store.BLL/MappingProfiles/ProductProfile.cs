using AutoMapper;
using Store.DAL;

namespace Store.BLL
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDtos>()  
                .ForMember(dest => dest.Category,
                       opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "No Category"));
            CreateMap<Product,CreateProductDtos>().ReverseMap();
            CreateMap<CreateProductDtos, Product>();
            CreateMap<Product,EditProductDtos>();
             
        }
    }
}
