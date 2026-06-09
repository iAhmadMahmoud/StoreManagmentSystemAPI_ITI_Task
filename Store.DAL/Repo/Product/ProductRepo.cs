using Microsoft.EntityFrameworkCore;
using Store.Common;

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

        public async Task<PagedResult<Product>> GetAllPaginationAsync
            (
                PaginationParameters? paginationParameters,
                ProductFilterParameters? filterParameters
            )
        {
            var query = _context.Set<Product>().AsQueryable();
            query = query.Include(p=>p.Category);

            if(filterParameters != null)
            {
                query = ApplyFilter(query, filterParameters);
            }

            var totalCount = await query.CountAsync();
            var pageNumber = paginationParameters?.PageNumber ?? 1;
            var pageSize = paginationParameters?.PageSize ?? 1;

            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 50);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<Product>
            {
                Items = items,
                Metadata = new PaginationMetadata
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalCount = totalCount,
                    HasNext = pageNumber < totalPages,
                    HasPrevious = pageNumber > 1,
                }

            };
        }
        private IQueryable<Product> ApplyFilter(IQueryable<Product> query,ProductFilterParameters filterParameters)
        {
            if (filterParameters.MinPrice > 0)
            {
                query = query.Where(p=>p.Price > filterParameters.MinPrice);
            }
            if (filterParameters.MaxPrice > 0)
            {
                query = query.Where(p=>p.Price < filterParameters.MaxPrice);
            }
            if (!string.IsNullOrEmpty(filterParameters.Search))
            {
                query = query.Where(p=>p.Title.Contains(filterParameters.Search));
            }
            return query;
        }

    }
}
