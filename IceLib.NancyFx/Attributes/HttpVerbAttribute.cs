using Nancy;
using Nancy.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IceLib.NancyFx.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public abstract class HttpVerbAttribute : Attribute
    {
        private readonly Type resource;

        public HttpVerbAttribute()
        {

        }        

        public const char ROUTE_SEPARATOR = '/';

        public Type Resource { get; set; }

        public HttpVerbAttribute(Type resource)
        {
            this.resource = resource;
        }

        public string ResourceName 
        {
            get
            {
                if (this.Resource != null) return this.Resource.Name;

                return null;
            }
        }

        protected abstract Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module);

        protected string CreateRouteParameter(string name)
        {
            var parameterFormat = @"{0}{{{1}}}";

            return string.Format(parameterFormat, ROUTE_SEPARATOR, name);
        }

        protected string CreateRoute(System.Reflection.MethodInfo method)
        {
            var route = new StringBuilder();

            route.Append(this.ResourceName ?? method.Name.ToLower());

            foreach (var parameter in method.GetParameters())
            {
                route.Append(CreateRouteParameter(parameter.Name));
            }

            return route.ToString().ToLower();
        }

        protected object[] GetMethodParameters(System.Reflection.MethodInfo method, dynamic _)
        {
            var parameters = new List<object>();

            foreach (var parameter in method.GetParameters())
            {
                var typedParameter = Convert.ChangeType(_[parameter.Name], parameter.ParameterType);

                parameters.Add(typedParameter);
            }

            return parameters.ToArray();
        }

        public void BindRoute(NancyModule module, System.Reflection.MethodInfo method)
        {
            var routeBuilder = this.GetRouteBuilder(module);

            var route = this.CreateRoute(method);

            routeBuilder[route] = _ =>
            {
                var parameters = GetMethodParameters(method, _);

                if (method.ReturnParameter.ParameterType == typeof(void))
                {
                    method.Invoke(this, parameters);

                    return module.Negotiate.WithStatusCode(HttpStatusCode.OK);
                }

                return method.Invoke(module, parameters);
            };
        }
    }
}