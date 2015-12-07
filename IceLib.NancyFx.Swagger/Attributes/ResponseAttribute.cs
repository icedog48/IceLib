using Nancy;
using IceLib.NancyFx.Swagger;
using System;
using IceLib.NancyFx.Swagger.Models;

namespace IceLib.NancyFx.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ResponseAttribute : Attribute
    {
        private Type _ReferenceType;
        private DataType _Type;

        public ResponseAttribute(HttpStatusCode code)
        {
            this.Code = code;
        }

        public HttpStatusCode Code { get; set; }

        public string Description { get; set; }

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

        public Models.Response AsModel
        {
            get
            {
                return new Models.Response()
                {
                    Description = this.Description,
                    ResponseCode = this.Code,
                    Schema = new Models.Schema()
                    {
                        Type = this.Type,
                        ReferenceType = this.ReferenceType
                    }
                };
            }
        }
    }
}
