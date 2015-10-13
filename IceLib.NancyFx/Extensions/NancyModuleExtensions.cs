using IceLib.NancyFx.Attributes;
using Nancy;
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
            var handlerMethods = module.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in handlerMethods)
            {
                var httpVerbAttributes = method.GetCustomAttributes(typeof(HttpVerbAttribute), true);

                foreach (var attribute in httpVerbAttributes)
                {
                    (attribute as HttpVerbAttribute).BindRoute(module, method);
                }
            }
        }
    }
}
