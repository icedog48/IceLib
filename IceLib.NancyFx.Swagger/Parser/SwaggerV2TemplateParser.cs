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
    public class SwaggerV2TemplateParser
    {
        public string ParseJSON(Models.SwaggerV2 swaggerV2)
        {
            var Swagger = System.Text.Encoding.UTF8.GetString(Templates.Swagger);

            var _Info = System.Text.Encoding.UTF8.GetString(Templates._Info);

            var _Path = System.Text.Encoding.UTF8.GetString(Templates._Path);

            var _Operation = System.Text.Encoding.UTF8.GetString(Templates._Operation);

            var _OperationResponse = System.Text.Encoding.UTF8.GetString(Templates._OperationResponse);

            var _Schema = System.Text.Encoding.UTF8.GetString(Templates._Schema);

            var razorService = Engine.Razor;
                razorService.AddTemplate("_Schema", _Schema);
                razorService.AddTemplate("_OperationResponse", _OperationResponse);
                razorService.AddTemplate("_Operation", _Operation);
                razorService.AddTemplate("_Path", _Path);
                razorService.AddTemplate("_Info", _Info);
                razorService.AddTemplate("Swagger", Swagger);

            return Engine.Razor.RunCompile(Swagger, "Swagger.cshtml", null, swaggerV2);
        }
    }
}
