using Microsoft.EntityFrameworkCore;

namespace Store.DAL
{
    public class ProductRepo : GenericRepo<Product>,IProductRepo
    {
        
        public ProductRepo(AppDbContext context):base(context) { }
       
   
        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products.Include(e=>e.Category).ToListAsync();
        }


        public async Task<Product?> GetByIdWithCategoryAsync(int prodId)
        {
            return await _context.Products.Include(c=>c.Category).FirstOrDefaultAsync(e=>e.Id == prodId);
        }

    }
}
