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
        public const string ROUTE_SEPARATOR = "/";

        private readonly Type resource;

        #region Constructors

        public HttpVerbAttribute()
        {

        }

        public HttpVerbAttribute(Type resource)
        {
            this.resource = resource;
        }

        #endregion Constructors

        #region Properties

        public Type Resource { get; set; }

        public string ResourceName
        {
            get
            {
                if (this.Resource != null) return this.Resource.Name;

                return null;
            }
        }

        #endregion Properties

        #region Methods

        protected abstract Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module);

        protected string CreateRouteParameter(string name)
        {
            var parameterFormat = @"{{{1}}}";

            return string.Format(parameterFormat, name);
        }        

        protected string CreateRoute(NancyModule module, System.Reflection.MethodInfo method)
        {
            var route = new StringBuilder();

            AddRoutePath(route, module.ModulePath);
            AddRoutePath(route, this.ResourceName ?? method.Name.ToLower());

            foreach (var parameter in method.GetParameters())
            {
                AddRoutePath(route, CreateRouteParameter(parameter.Name));
            }

            return route.ToString().ToLower();
        }

        protected object[] GetMethodParameters(System.Reflection.MethodInfo method, dynamic requestParameters)
        {
            var parameters = new List<object>();

            foreach (var parameter in method.GetParameters())
            {
                var typedParameter = Convert.ChangeType(requestParameters[parameter.Name], parameter.ParameterType);

                parameters.Add(typedParameter);
            }

            return parameters.ToArray();
        }

        public static void AddRoutePath(StringBuilder route, string path)
        {
            route.Append(string.Join(ROUTE_SEPARATOR, path));
        }

        public void BindRoute(NancyModule module, System.Reflection.MethodInfo method)
        {
            var routeBuilder = this.GetRouteBuilder(module);

            var route = this.CreateRoute(module, method);

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

        #endregion Methods
    }
}