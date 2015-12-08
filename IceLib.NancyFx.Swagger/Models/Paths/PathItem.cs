using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using IceLib.NancyFx.Extensions;
using IceLib.NancyFx.Swagger.Extensions;
using IceLib.NancyFx.Helpers;

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

        public static PathItem GetPathItem(string modulePath, System.Reflection.MethodInfo method)
        {
            var pathItem = new PathItem();

            var verb = method.GetHttpVerbAttributes().FirstOrDefault();

            if (verb != null)
            {
                pathItem.Url = new RouteHelper()
                                    .AddPath(modulePath)
                                    .AddPath(verb.ActionPath)
                                        .ToString();

                var operation = new Operation()
                {
                    Description = verb.Description,
                    HttpMethod = verb.Method,
                    Produces = verb.Produces
                };

                foreach (var parameter in method.GetParameterAttributes())
                    operation.Parameters.Add(parameter.AsModel);

                foreach (var operationResponse in method.GetResponseAttributes())
                    operation.Responses.Add(operationResponse.AsModel);

                pathItem.Operations.Add(operation);
            }

            return pathItem;
        }
    }
}
