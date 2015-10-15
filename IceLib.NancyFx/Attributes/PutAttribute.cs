using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IceLib.NancyFx.Attributes
{
    public class PutAttribute : HttpVerbAttribute
    {
        public PutAttribute()
        {

        }

        public PutAttribute(string route)
            : base(route)
        {

        }

        public override Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module)
        {
            return module.Put;
        }
    }
}