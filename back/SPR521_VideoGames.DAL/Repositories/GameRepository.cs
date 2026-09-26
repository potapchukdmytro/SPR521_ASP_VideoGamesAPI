using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.DAL.Repositories
{
    public class GameRepository : GenericRepository<Game>
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
