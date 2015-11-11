using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Storage
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Items { get; }

        IQueryable<TEntity> ActiveItems { get; set; }

        void Add(TEntity entity);

        void Add(ICollection<TEntity> entities);

        void Remove(TEntity entity);

        void Remove(ICollection<TEntity> entities);

        void Update(TEntity entity);

        void Update(ICollection<TEntity> entities);

        TEntity Get(object id); 

        void ExecuteTransaction(Action action) ;
    }
}
