using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeApp.API.Helpers;
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
            if (home != null)
             {
                home.Photos = home.Photos.OrderByDescending(p => p.IsCover).ToList();  //ToList() here works in in memory data, not hitting database again
             }
        
            return home;
        }

        public async Task<PagedList<Home>> GetHomes(HomeParams homeParams)
        {
            var homes = _context.Homes.Include(h => h.Photos).Include(h => h.User).Include(h => h.Favorites).OrderByDescending(h => h.DateAdded).AsQueryable();

            if (homeParams.Bed != 1)
            {
                homes = homes.Where(h => h.Bedrooms >= homeParams.Bed);
            }

            if (homeParams.Bath != 1)
            {
                homes = homes.Where(h => h.Bathrooms >= homeParams.Bath);
            }

            if (homeParams.MinPrice != 1 || homeParams.MaxPrice != 10000000)
            {
                homes = homes.Where(h => h.Price >= homeParams.MinPrice && h.Price <= homeParams.MaxPrice);
            }

            if (!string.IsNullOrEmpty(homeParams.SearchQuery))
            {
                var searchQueryClause = homeParams.SearchQuery.Trim().ToLowerInvariant();
                homes = homes.Where(h => 
                h.City.ToLowerInvariant().Contains(searchQueryClause) ||
                h.Street.ToLowerInvariant().Contains(searchQueryClause) ||
                h.State.ToLowerInvariant().Contains(searchQueryClause)
                );
            }

            if (!string.IsNullOrEmpty(homeParams.OrderBy))
            {
                switch (homeParams.OrderBy.Trim().ToLowerInvariant())
                {
                    case "priceasc":
                        homes = homes.OrderBy(h => h.Price);
                        break;
                    case "pricedesc":
                        homes = homes.OrderByDescending(h => h.Price);
                        break;
                    default:
                        homes = homes.OrderBy(h => h.DateAdded);
                        break;
                }
            }

            return await PagedList<Home>.CreatePagedListAsync(homes, homeParams.CurrentPage, homeParams.PageSize);
        }

        public async Task<IEnumerable<Home>> GetHomesByUser(int id)
        {
            return await _context.Homes.Include(h => h.Photos).Include(h => h.User).Where(h => h.User.Id == id).OrderByDescending(h => h.DateAdded).ToListAsync();

        }

        public async Task<PagedList<Home>> GetFavHomesByUser(int id, HomeParams homeParams)
        {
            var user = await _context.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Id == id);
            var userFavHomeIds = user.Favorites.Select(f => f.HomeId);
            var homes = _context.Homes.Include(h => h.Photos).Include(h => h.User).Where(h => userFavHomeIds.Contains(h.Id)).OrderByDescending(h => h.DateAdded).AsQueryable();
            return await PagedList<Home>.CreatePagedListAsync(homes, homeParams.CurrentPage, homeParams.PageSize);
        }

         public async Task<Photo> GetPhoto(int homeId, int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.HomeId == homeId && p.Id == id);
        }

        public async Task<Photo> GetCoverPhotoForHome(int homeId)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.IsCover && p.HomeId == homeId);
        }

        public async Task<Favorite> GetFavorite(int userId, int homeId)
        {
            return await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.HomeId == homeId);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}