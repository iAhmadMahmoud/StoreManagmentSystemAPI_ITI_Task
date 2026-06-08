using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.DAL
{
    public class GenericRepo<T> : IGenericRepo<T> where T :class
    {
        protected readonly AppDbContext _context;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllGenericAsunc
            (
            Expression<Func<T, bool>>? filter = null,
            bool trackChanges = false
            )
        {
            IQueryable<T> query = _context.Set<T>();
            
            if( filter is not null)
            {
                query = query.Where(filter);
            }
            if( !trackChanges)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Add(T _T)
        {
            _context.Set<T>().Add(_T);
        }

        public void Delete(T _T)
        {
            _context.Set<T>().Remove(_T);
        }

    }
}
