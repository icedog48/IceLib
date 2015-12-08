using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Models
{
    public class Schema
    {
        public DataType Type { get; set; }

        public Type ReferenceType { get; set; }

        public string TypeAsString { get { return this.Type.ToString(); } }

        public int TypeAsInt { get { return (int)this.Type; } }

        public string ReferenceTypeAsString
        {
            get 
            {
                return (this.ReferenceType.IsArray) ? this.ReferenceType.GetElementType().Name : this.ReferenceType.Name;
            }
        }
    }
}
