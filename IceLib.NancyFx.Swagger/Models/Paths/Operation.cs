using IceLib.NancyFx.Swagger.Models.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Models
{
    public class Operation
    {
        public Operation()
        {
            this.Parameters = new List<Parameter>();
            this.Responses = new List<OperationResponse>();
        }

        public string Description { get; set; }
        public string HttpMethod { get; set; }
        public string Produces { get; set; }

        public IList<Parameter> Parameters { get; set; }
        public IList<OperationResponse> Responses { get; set; }
    }
}
