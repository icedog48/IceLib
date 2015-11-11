using IceLib.NancyFx.Attributes;
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
    public static class MethodInfoExtensions
    {
        public static object[] GetActionParameters(this MethodInfo method, DynamicDictionary requestParameters)
        {
            var parameters = new List<object>();

            foreach (var parameter in method.GetParameters())
            {
                try
                {
                    var typedParameter = Convert.ChangeType(requestParameters[parameter.Name], parameter.ParameterType);

                    parameters.Add(typedParameter);
                }
                catch (Exception ex)
                {
                    var errMsg = string.Format("Error casting parameter '{0}' from action '{1}'.", parameter.Name, method.Name);

                    throw new InvalidOperationException(errMsg, ex);
                }
            }

            return parameters.ToArray();
        }
    }
}
