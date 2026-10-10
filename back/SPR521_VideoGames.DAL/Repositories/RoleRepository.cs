using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Role> Roles => GetAll();
    }
}
