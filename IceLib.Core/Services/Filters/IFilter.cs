using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IceLib.Service.Filters
{
    public interface IFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
