using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.DAL.Repositories
{
    public class UserTokenRepository : GenericRepository<UserToken>
    {
        public UserTokenRepository(AppDbContext context)
            : base(context)
        {
            
        }
    }
}
