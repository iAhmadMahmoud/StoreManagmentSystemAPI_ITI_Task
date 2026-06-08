using System.Linq.Expressions;

namespace Store.DAL
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllGenericAsunc
            (
            Expression<Func<T,bool>>? filter = null,
            bool trackChanges = false
            );
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        void Add(T _T);
        void Delete(T _T);
    }
}
