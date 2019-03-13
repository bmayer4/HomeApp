using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.API.Models;

namespace HomeApp.API.Data
{
    public interface IHomeRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<User> GetUser(int id);
        Task<Home> GetHome(int id);
        Task<Photo> GetPhoto(int homeId, int id);
        Task<Photo> GetCoverPhotoForHome(int homeId);
        Task<IEnumerable<Home>> GetHomes();
        Task<IEnumerable<User>> GetProfessionalUsers();

    }
}