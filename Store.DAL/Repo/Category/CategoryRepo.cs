namespace Store.DAL
{
    public class CategoryRepo : GenericRepo<Category>,ICategoryRepo
    {

        public CategoryRepo(AppDbContext context):base(context) { }


    }
}
