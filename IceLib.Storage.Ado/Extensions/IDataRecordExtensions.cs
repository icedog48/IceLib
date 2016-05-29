using IceLib.Storage.Ado.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Storage.Ado.Extensions
{
    public static class IDataRecordExtensions
    {
        public static Dictionary<string, int> GetColumnNames(this IDataRecord record)
        {
            var result = new Dictionary<string, int>();

            for (int i = 0; i < record.FieldCount; i++)
            {
                result.Add(record.GetName(i), i);
            }

            return result;
        }

        public static bool HasColumn(this IDataRecord dataRecord, string columnName)
        {
            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                if (dataRecord.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static T MapType<T>(this IDataRecord record) where T : new()
        {
            var objT = new T();
            
            foreach (var property in typeof(T).GetProperties())
            {
                var columnName = property.Name;

                //Caso o nome da coluna na database for diferente do nome da propriedade
                object[] attrs = property.GetCustomAttributes(true);

                if (attrs != null && attrs.Any(x => x is ColumnNameAttribute))
                {
                    var columnNameAttribute = attrs.FirstOrDefault(x => x is ColumnNameAttribute) as ColumnNameAttribute;

                    columnName = columnNameAttribute.Value;
                }

                if (record.HasColumn(columnName) && !record.IsDBNull(record.GetOrdinal(columnName)))
                {
                    property.SetValue(objT, record[columnName]);
                }
            }

            return objT;
        }
    }
}
