using IceLib.NancyFx.Swagger.Models;
using IceLib.NancyFx.Swagger.Parser;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Nancy
{
    public abstract class SwaggerModule : NancyModule
    {
        public SwaggerModule(string path) : base(path)
        {
            Get["/"] = _ => 
            {
                var assembly = Assembly.GetAssembly(this.GetType());

                var swagger = SwaggerV2.CreateInstance(assembly);

                var json = SwaggerV2JsonConverter.SerializeObject(swagger);

                var jsonBytes = Encoding.UTF8.GetBytes(json);

                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            };
        }
    }
}
