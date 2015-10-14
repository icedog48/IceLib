using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IceLib.NancyFx.Attributes
{
    public class GetAttribute : HttpVerbAttribute
    {
        public GetAttribute()
        {

        }

        public GetAttribute(Type resource) : base(resource)
        {

        }

        public override Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module)
        {
            return module.Get;
        }
    }
}