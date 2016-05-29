using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Storage.Ado
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
