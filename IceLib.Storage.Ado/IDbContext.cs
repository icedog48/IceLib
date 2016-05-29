using System;
using System.Data;

namespace IceLib.Storage.Ado
{
    public interface IDbContext : IDisposable
    {
        IDbCommand CreateCommand();

        IUnitOfWork CreateUnitOfWork();
    }
}