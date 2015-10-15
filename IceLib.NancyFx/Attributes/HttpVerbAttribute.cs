using IceLib.NancyFx.Helpers;

using Nancy;
using Nancy.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace IceLib.NancyFx.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public abstract class HttpVerbAttribute : Attribute
    {
        private string route;

        #region Constructors

        public HttpVerbAttribute()
        {

        }

        public HttpVerbAttribute(string route)
        {
            this.route = route;
        }

        #endregion Constructors

        #region Properties

        public string Route
        {
            get
            {
                return route;
            }
        }

        #endregion Properties

        #region Methods

        public abstract Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module);               

        public void BindRoute(NancyModule module, MethodInfo method)
        {
            var route = new RouteHelper()
                .AddPath(module.ModulePath)
                .AddPath(this.Route ?? string.Empty)
                .ToString();

            var routeBuilder = GetRouteBuilder(module);

                routeBuilder[route] = requestParameters =>
                {
                    var methodParameters = ReflectionHelper.GetMethodParameters(method, requestParameters);
                    
                    return ReflectionHelper.Invoke(module, method, methodParameters);
                };
        }

        #endregion Methods
    }
}