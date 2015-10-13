using FluentNHibernate.Mapping;
using IceLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Storage.Nhibernate.Mapping
{
    public abstract class LogicalExclusionEntityMap<T> : EntityMap<T> where T : LogicalExclusionEntity
    {
        public LogicalExclusionEntityMap()
        {
            Map(x => x.SituacaoRegistro);
        }
    }
}
