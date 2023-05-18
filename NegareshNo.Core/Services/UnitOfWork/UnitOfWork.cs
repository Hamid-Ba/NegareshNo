using NegareshNo.Core.Services.Repository;
using NegareshNo.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        public readonly NegareshNoContext Context;

        public UnitOfWork(NegareshNoContext context) => Context = context;

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            IRepository<TEntity> repository = new Repository<TEntity, NegareshNoContext>(Context); ;
            return repository;
        }

        public async Task CommitAsync() => await Context.SaveChangesAsync();
        public void Commit() => Context.SaveChanges();
        public void Dispose() => Context.Dispose();
    }
}
