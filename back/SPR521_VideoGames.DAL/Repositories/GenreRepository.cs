using Microsoft.EntityFrameworkCore;
using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.DAL.Repositories
{
    public class GenreRepository : GenericRepository<Genre>
    {
        public GenreRepository(AppDbContext context) 
            : base(context)
        {
        }

        public IQueryable<Genre> Genres => GetAll();

        public async Task<bool> IsExistsAsync(string name, CancellationToken ct = default)
        {
            return await Genres.AnyAsync(g => g.Name.ToLower() == name.ToLower(), ct);
        }

        public async Task<Genre?> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await Genres.FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower(), ct);
        }
    }
}
