using System.Linq;
using MusicPlayer.Data.Entities;

namespace MusicPlayer.Data.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T Add(T entity);
        IQueryable<T> Get();
        IQueryable<T> GetById(int id);
        int Save();
        void Update(T entity);
    }
}