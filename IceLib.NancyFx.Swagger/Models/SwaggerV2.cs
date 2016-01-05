﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RazorEngine;
using RazorEngine.Templating;

using IceLib.NancyFx;
using IceLib.NancyFx.Extensions;
using IceLib.NancyFx.Swagger.Resources;
using IceLib.NancyFx.Swagger.Extensions;
using IceLib.NancyFx.Modules;

using System.Reflection;
using IceLib.NancyFx.Helpers;
using Newtonsoft.Json;

namespace IceLib.NancyFx.Swagger.Models
{
    public class SwaggerV2
    {
        public SwaggerV2()
        {
            Info = new APIInfo();

            Paths = new List<PathItem>();
        }

        [JsonProperty("swagger")]
        public string SpecsVersion { get { return "2.0"; } }

        [JsonProperty("info")]
        public APIInfo Info { get; set; }

        [JsonProperty("paths")]
        public IList<PathItem> Paths { get; set; }

        [JsonProperty("definitions")]
        public IList<Type> Definitions { get; set; }

        public static SwaggerV2 CreateInstance(Assembly assembly) 
        {
            var swagger = new SwaggerV2();

            var modules = assembly.GetInstances<APIModule>();

            foreach (var module in modules)
            {
                var methods = module.GetActionMethods();

                foreach (var method in methods)
                {
                    var pathItem = PathItem.GetPathItem(module.ModulePath, method);

                    swagger.Paths.Add(pathItem);
                }
            }

            return swagger;
        }
    }
}
