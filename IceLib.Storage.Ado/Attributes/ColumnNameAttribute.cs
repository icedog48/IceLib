using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Storage.Ado.Attributes
{
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }
}
