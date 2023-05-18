using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.Repository
{
    public interface IRepository<TEntity>
    {
        //Read ALL
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
        IEnumerable<TEntity> GetAllEntities();

        //Read ONE
        Task<TEntity> GetEntityByIdAsync(object id);
        TEntity GetEntityById(object id);

        //Create One&Range
        object AddEntity(TEntity entity);
        void AddRangeOfEntities(IEnumerable<TEntity> entities);
        Task<object> AddEntityAsync(TEntity entity);
        Task AddRangeOfEntitiesAsync(IEnumerable<TEntity> entities);

        //Update One&Range
        bool UpdateEntity(TEntity entity);
        void UpdateRangeOfEntities(IEnumerable<TEntity> entities);

        //Delete One&Range
        bool DeleteEntity(TEntity entity);
        void DeleteRangeOfEntities(IEnumerable<TEntity> entities);

        int GetCountOfEntity();
        IEnumerable<TEntity> PaginationOfEntity(int currentPage, int pageSize);
    }
}
