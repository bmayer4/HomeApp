using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeApp.API.Data
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AppDbContext _context;

        public HomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Home> GetHome(int id)
        {
            var home = await _context.Homes.Include(h => h.Photos).Include(h => h.User).FirstOrDefaultAsync(h => h.Id == id);
            // eager loading (Include) doesn't support any filtering or sorting of loaded child collections
            home.Photos = home.Photos.OrderByDescending(p => p.IsCover).ToList();  //ToList() here works in in memory data, not hitting database again
            return home;
        }

        public async Task<IEnumerable<Home>> GetHomes()
        {
            return await _context.Homes.Include(h => h.Photos).Include(h => h.User).OrderByDescending(h => h.DateAdded).ToListAsync();
        }

        public async Task<IEnumerable<Home>> GetHomesByUser(int id)
        {
            return await _context.Homes.Include(h => h.Photos).Include(h => h.User).Where(h => h.User.Id == id).OrderByDescending(h => h.DateAdded).ToListAsync();
        }

         public async Task<Photo> GetPhoto(int homeId, int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.HomeId == homeId && p.Id == id);
        }

        public async Task<Photo> GetCoverPhotoForHome(int homeId)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.IsCover && p.HomeId == homeId);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}