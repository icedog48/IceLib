using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Swagger.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Extensions
{
    public static class MethodInfoExtensions
    {
        public static HttpVerbAttribute[] GetHttpVerbAttributes(this MethodInfo method)
        {
            return method.GetCustomAttributes(typeof(HttpVerbAttribute), true) as HttpVerbAttribute[];
        }

        public static ParameterAttribute[] GetParameterAttributes(this MethodInfo method)
        {
            return method.GetCustomAttributes(typeof(ParameterAttribute), true) as ParameterAttribute[];
        }

        public static ResponseAttribute[] GetResponseAttributes(this MethodInfo method)
        {
            return method.GetCustomAttributes(typeof(ResponseAttribute), true) as ResponseAttribute[];
        }

        public static IList<T> GetInstances<T>(this Assembly assembly)
        {
            return (from t in assembly.GetTypes()
                    where t.BaseType == (typeof(T)) && t.GetConstructor(Type.EmptyTypes) != null
                    select (T)Activator.CreateInstance(t)).ToList();
        }
    }
}
