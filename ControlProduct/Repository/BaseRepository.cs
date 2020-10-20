using ControlProduct.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlProduct.Repository
{
    public class BaseRepository<TEntity>
        where TEntity: class
    {
        public BaseContext Context { get; }

        public DbSet<TEntity> Entity { get; }

        public BaseRepository(BaseContext context)
        {
            Context = context;
            Entity = Context.Set<TEntity>();
        }

        public async Task<List<TEntity>> FromQuery(string query, params object[] args)
        {
            return await Entity.FromSqlRaw(query, args).ToListAsync();
        }

        public async Task Insert(TEntity entity)
        {
            Entity.Add(entity);
            Context.SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            Entity.Update(entity);
            Context.SaveChanges();
        }

        public async Task Delete(TEntity entity)
        {
            Entity.Remove(entity);
            Context.SaveChanges();
        }
    }
}
