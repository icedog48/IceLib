using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IceLib.NancyFx.Attributes
{
    public class PostAttribute : HttpVerbAttribute
    {
        public PostAttribute()
        {

        }

        public PostAttribute(Type resource) : base(resource)
        {

        }

        public override Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module)
        {
            return module.Post;
        }
    }
}