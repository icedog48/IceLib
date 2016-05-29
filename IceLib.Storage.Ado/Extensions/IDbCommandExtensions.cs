using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Storage.Ado.Extensions
{
    public static class IDbCommandExtensions
    {
        public static IDbDataParameter CreateParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();

            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;

            return parameter;
        }

        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            command.Parameters.Add(command.CreateParameter(name, value));
        }
    }
}
