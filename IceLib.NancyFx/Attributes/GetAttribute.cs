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

        public GetAttribute(string route)
            : base(route)
        {

        }

        public override Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module)
        {
            return module.Get;
        }
    }
}