using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IceLib.Services.Filters
{
    public interface IFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
