using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Modules;
using IceLib.NancyFx.Swagger.Attributes;
using IceLib.NancyFx.Swagger.Nancy;
using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Tests.Modules
{
    public class SwaggerV2TestModule : SwaggerModule
    {
        public SwaggerV2TestModule() : base("api/v1/swagger") 
        {
        
        }
    }
}
