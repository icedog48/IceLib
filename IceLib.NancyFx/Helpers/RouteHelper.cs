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
    public class RouteHelper 
    {
        private StringBuilder route;

        public RouteHelper()
        {
            route = new StringBuilder();
        }

        public const string ROUTE_SEPARATOR = "/";

        public const string ROUTE_PARAMETER_FORMAT = @"{{{1}}}";

        public RouteHelper AddParameter(string paremeterName)
        {
            route.Append(string.Format(ROUTE_PARAMETER_FORMAT, paremeterName));

            return this;
        }

        public RouteHelper AddPath(string path)
        {
            var routePath = path;

            if (!routePath.StartsWith(ROUTE_SEPARATOR))
	        {
		        routePath = string.Concat(ROUTE_SEPARATOR, path);
	        }

            route.Append(routePath);

            return this;
        }

        public override string ToString()
        {
            return route.ToString();
        }
    }
}
