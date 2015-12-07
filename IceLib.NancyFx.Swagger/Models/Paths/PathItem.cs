using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Models
{
    public class PathItem
    {
        public PathItem()
        {
            this.Operations = new List<Operation>();
        }

        public string Url { get; set; }

        public IList<Operation> Operations { get; set; }
    }
}
