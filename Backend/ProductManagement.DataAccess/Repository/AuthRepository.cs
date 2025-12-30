namespace ProductManagement.DataAccess.Repository
{
    using ProductManagement.DataAccess.IRepository;
    using ProductManagement.Entities.DataContext;
    using ProductManagement.Entities.DataModels;

    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        public AuthRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}