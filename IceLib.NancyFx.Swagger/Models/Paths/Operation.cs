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
        public string Description { get; set; }
        public string HttpMethod { get; set; }
        public string Produces { get; set; }

        public IList<Response> Responses { get; set; }
    }
}
