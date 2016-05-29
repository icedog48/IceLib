
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IceLib.Storage.Ado.Extensions;

namespace IceLib.Storage.Ado
{
    public abstract class Repository<TEntity> where TEntity : new()
    {
        private readonly IDbContext _context;

        public Repository(IDbContext context)
        {
            _context = context;
        }

        protected IDbContext Context
        {
            get
            {
                return this._context;
            }
        }

        protected IEnumerable<TEntity> ExecuteReader(IDbCommand command)
        {
            using (var record = command.ExecuteReader())
            {
                List<TEntity> items = new List<TEntity>();

                while (record.Read())
                {
                    items.Add(record.MapType<TEntity>());
                }

                return items;
            }
        }
    }
}
