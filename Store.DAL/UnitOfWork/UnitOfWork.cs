
namespace Store.DAL
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepo ProductRepo { get; }
        public ICategoryRepo CategoryRepo { get; }

        public UnitOfWork(IProductRepo productRepo, ICategoryRepo categoryRepo, AppDbContext context)
        {
            ProductRepo = productRepo;
            CategoryRepo = categoryRepo;
            _context = context;
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
