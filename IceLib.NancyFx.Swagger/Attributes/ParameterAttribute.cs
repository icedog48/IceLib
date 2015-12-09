using IceLib.NancyFx.Swagger.Models;
using IceLib.NancyFx.Swagger.Models.Paths;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ParameterAttribute : Attribute
    {
        private Type _ReferenceType;
        private DataType _Type;

        public ParameterAttribute(ParameterType parameterIn)
        {
            this.In = parameterIn;

            if (this.In == ParameterType.Body)
            {
                this.Name = ParameterType.Body.ToString();
            }   
                     
            this.Type = DataType.Integer;
        }

        public ParameterType In { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        public DataType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;

                this.ReferenceType = null;
            }
        }

        public Type ReferenceType
        {
            get { return _ReferenceType; }
            set
            {
                _ReferenceType = value;

                if (_ReferenceType != null)
                {
                    this.Type = DataType.ReferenceType;
                }
            }
        }

        public OperationParameter AsModel
        {
            get
            {
                return new OperationParameter()
                {
                    Description = this.Description,
                    In = this.In,
                    Name = this.Name,
                    Required = this.Required,
                    Schema = new Schema()
                    {
                        Type = this.Type,
                        ReferenceType = this.ReferenceType
                    }
                };
            }
        }
    }
}
