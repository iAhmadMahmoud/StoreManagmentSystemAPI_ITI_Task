namespace Store.DAL
{
    public interface IUnitOfWork
    {
        IProductRepo ProductRepo { get; }
        ICategoryRepo CategoryRepo { get; }
        Task SaveChangesAsync();
    }
}
