using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Models
{
    public class SwaggerV2
    {
        public SwaggerV2()
        {
            Info = new APIInfo();

            Paths = new List<PathItem>();
        }

        public APIInfo Info { get; set; }

        public IList<PathItem> Paths { get; set; }
    }
}
