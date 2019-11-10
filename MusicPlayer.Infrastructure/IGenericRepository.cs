using System.Linq;
using MusicPlayer.Core;

namespace MusicPlayer.Infrastructure
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