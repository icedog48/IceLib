using FluentNHibernate.Mapping;
using IceLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Storage.Nhibernate.Mapping
{
    public abstract class EntityMap<T> : ClassMap<T> where T : Entity
    {
        public EntityMap()
        {
            Id(x => x.Id);
        }
    }
}
