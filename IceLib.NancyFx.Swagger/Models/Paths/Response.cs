using Nancy;
using System;

namespace IceLib.NancyFx.Swagger.Models
{
    public class Response
    {
        public HttpStatusCode ResponseCode { get; set; }

        public string Description { get; set; }

        public Schema Schema { get; set; }

        public int ResponseCodeAsInt { get { return (int)this.ResponseCode; } }
    }
}
