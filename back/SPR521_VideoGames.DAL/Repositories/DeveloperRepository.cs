using Microsoft.EntityFrameworkCore;
using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.DAL.Repositories
{
    public class DeveloperRepository : GenericRepository<Developer>
    {
        private readonly AppDbContext _context;

        public DeveloperRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IQueryable<Developer> Developers => GetAll();

        public async Task<bool> IsExistAsync(int id, CancellationToken ct = default)
        {
            return await Developers.AnyAsync(d => d.Id == id, ct);
        }
    }
}
