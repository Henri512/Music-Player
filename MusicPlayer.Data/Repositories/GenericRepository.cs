using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Data.Entities;
using System.Linq;
using MusicPlayer.Model.Models;

namespace MusicPlayer.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetById(int id)
        {
            return _dbSet.Where(e => e.Id == id);
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
