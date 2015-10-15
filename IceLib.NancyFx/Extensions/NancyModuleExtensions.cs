using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Helpers;
using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Extensions
{
    public static class NancyModuleExtensions
    {
        public static void BindRoutes(this NancyModule module)
        {
            var handlerMethods = ReflectionHelper.GetRouteMethods(module);

            foreach (MethodInfo method in handlerMethods)
            {
                ReflectionHelper.BindRoute(module, method);
            }
        }
    }
}
