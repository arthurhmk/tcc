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
    }
}
