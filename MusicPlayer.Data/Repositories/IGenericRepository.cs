using System.Collections.Generic;
using System.Linq;
using MusicPlayer.Data.Entities;
using MusicPlayer.Model.Models;

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