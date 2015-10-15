using IceLib.NancyFx.Attributes;
using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Helpers
{
    public static class ReflectionHelper
    {
        public static MethodInfo[] GetRouteMethods(NancyModule module)
        {
            return module.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        }

        public static object[] GetMethodParameters(MethodInfo method, dynamic requestParameters)
        {
            var parameters = new List<object>();

            foreach (var parameter in method.GetParameters())
            {
                var typedParameter = Convert.ChangeType(requestParameters[parameter.Name], parameter.ParameterType);

                parameters.Add(typedParameter);
            }

            return parameters.ToArray();
        }

        public static Negotiator Invoke(NancyModule module, MethodInfo method, object[] parameters)
        {
            if (method.ReturnParameter.ParameterType == typeof(void))
            {
                method.Invoke(module, parameters);

                return module.Negotiate.WithStatusCode(HttpStatusCode.OK);
            }

            return method.Invoke(module, parameters) as Negotiator;
        }

        public static void BindRoute(NancyModule module, MethodInfo method)
        {
            var httpVerbAttributes = method.GetCustomAttributes(typeof(HttpVerbAttribute), true);

            foreach (var attribute in httpVerbAttributes)
            {
                (attribute as HttpVerbAttribute).BindRoute(module, method);
            }
        }
    }
}
