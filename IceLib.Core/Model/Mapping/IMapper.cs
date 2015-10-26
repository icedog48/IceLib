using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Core.Model.Mapping
{
    public interface IMapper<TSource, TTarget>
        where TSource : new()
        where TTarget : new()
    {
        TTarget Map(TSource source);
    }
}
