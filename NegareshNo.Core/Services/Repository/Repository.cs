using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.Repository
{
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        readonly DbSet<TEntity> dBSet;
        readonly TContext context;

        public Repository(TContext context)
        {
            this.context = context;
            dBSet = this.context.Set<TEntity>();
        }

        //Read
        public IEnumerable<TEntity> GetAllEntities() => dBSet.AsNoTracking().ToList();
        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync() => await dBSet.AsNoTracking().ToListAsync();

        public TEntity GetEntityById(object id) => dBSet.Find(id);
        public async Task<TEntity> GetEntityByIdAsync(object id) => await dBSet.FindAsync(id);

        //Create
        public object AddEntity(TEntity entity) => dBSet.Add(entity);
        public void AddRangeOfEntities(IEnumerable<TEntity> entities) => dBSet.AddRange(entities);
        public async Task<object> AddEntityAsync(TEntity entity) => await dBSet.AddAsync(entity);
        public async Task AddRangeOfEntitiesAsync(IEnumerable<TEntity> entities) => await dBSet.AddRangeAsync(entities);

        //Update
        public bool UpdateEntity(TEntity entity)
        {
            try
            {
                dBSet.Update(entity);
                return true;
            }
            catch { return false; }
        }
        public void UpdateRangeOfEntities(IEnumerable<TEntity> entities) => dBSet.UpdateRange(entities);

        //Delete    
        public bool DeleteEntity(TEntity entity)
        {
            try
            {
                dBSet.Remove(entity);
                return true;
            }
            catch { return false; }
        }
        public void DeleteRangeOfEntities(IEnumerable<TEntity> entities) => dBSet.RemoveRange(entities);

        public int GetCountOfEntity() => dBSet.AsNoTracking().Count();

        public IEnumerable<TEntity> PaginationOfEntity(int currentPage, int pageSize)
        {
            var entities = GetAllEntities();
            return entities.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
