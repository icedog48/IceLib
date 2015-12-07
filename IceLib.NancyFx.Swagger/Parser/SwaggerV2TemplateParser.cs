using IceLib.NancyFx.Swagger.Resources;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Parser
{
    public class SwaggerV2TemplateParser : ISwaggerV2Parser
    {
        public string ParseJSON(Models.SwaggerV2 swaggerV2)
        {
            var Swagger = System.Text.Encoding.UTF8.GetString(Templates.Swagger);

            var _Info = System.Text.Encoding.UTF8.GetString(Templates._Info);

            var _Path = System.Text.Encoding.UTF8.GetString(Templates._Path);

            var razorService = Engine.Razor;
                razorService.AddTemplate("_Path", _Path);
                razorService.AddTemplate("_Info", _Info);
                razorService.AddTemplate("Swagger", Swagger);

            return Engine.Razor.RunCompile(Swagger, "Swagger.cshtml", null, swaggerV2);
        }
    }
}
