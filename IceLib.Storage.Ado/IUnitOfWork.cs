using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Storage.Ado
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
