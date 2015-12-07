using IceLib.NancyFx.Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Parser
{
    public interface ISwaggerV2Parser
    {
        string ParseJSON(SwaggerV2 swaggerV2);
    }
}
