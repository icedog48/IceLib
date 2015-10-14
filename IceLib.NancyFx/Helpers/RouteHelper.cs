using IceLib.NancyFx.Attributes;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Helpers
{
    public static class RouteHelper
    {
        public const string ROUTE_SEPARATOR = "/";

        public const string ROUTE_PARAMETER_FORMAT = @"{{{1}}}";

        public static void AddRouteParameter(StringBuilder route, string paremeterName)
        {
            route.Append(string.Format(ROUTE_PARAMETER_FORMAT, paremeterName));
        }

        public static void AddRoutePath(StringBuilder route, string path)
        {
            route.Append(string.Join(ROUTE_SEPARATOR, path));
        }

        public static void BindRoute(NancyModule module, MethodInfo method)
        {
            var httpVerbAttributes = method.GetCustomAttributes(typeof(HttpVerbAttribute), true);

            foreach (var attribute in httpVerbAttributes)
            {
                (attribute as HttpVerbAttribute).BindRoute(module, method);
            }
        }

        public static MethodInfo[] GetRouteMethods(NancyModule module)
        {
            return module.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        }

        public static string CreateRoute(string modulePath, string resourceName, ParameterInfo[] parameters)
        {
            var route = new StringBuilder();

            //Module Path
            RouteHelper.AddRoutePath(route, modulePath);

            //Resource Path
            RouteHelper.AddRoutePath(route, resourceName);

            //Parameters Path
            foreach (var parameter in parameters)
            {
                RouteHelper.AddRoutePath(route, parameter.Name);
            }

            return route.ToString().ToLower();
        }
    }
}
