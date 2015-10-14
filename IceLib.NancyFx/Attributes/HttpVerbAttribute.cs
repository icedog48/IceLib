using IceLib.NancyFx.Helpers;
using IceLib.NancyFx.Extensions;

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

        public abstract Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module);               

        public void BindRoute(NancyModule module, MethodInfo method)
        {
            var routeBuilder = GetRouteBuilder(module);

            var route = RouteHelper.CreateRoute(module.ModulePath, this.ResourceName ?? method.Name.ToLower(), method.GetParameters());

            routeBuilder[route] = requestParameters =>
            {
                var methodParameters = ReflectionHelper.GetMethodParameters(method, requestParameters);

                return ReflectionHelper.Invoke(module, method, methodParameters);
            };
        }

        #endregion Methods
    }
}