using GymApi.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GymApi.Repository
{
    public class CommonRepository<T> : IRepository<T> where T : class
    {
        private GymContext _context;
        private DbSet<T> _dbSet;
        public CommonRepository(GymContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
            
        }
        public async Task<IEnumerable<T>> Get()
            => await _dbSet.ToListAsync();


        public async Task<T>GetById(int id)
            => await _dbSet.FindAsync(id);
        public async Task Create(T entity)
        => await _dbSet.AddAsync(entity);

        public void Delete(T entity) 
            => _dbSet.Remove(entity);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> Search(Func<T, bool> filter)
        {
            return _dbSet.Where(filter).ToList();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
