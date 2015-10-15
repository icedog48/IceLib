﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IceLib.NancyFx.Attributes
{
    public class DeleteAttribute : HttpVerbAttribute
    {
        public DeleteAttribute()
        {

        }

        public DeleteAttribute(string route)
            : base(route)
        {

        }

        public override Nancy.NancyModule.RouteBuilder GetRouteBuilder(Nancy.NancyModule module)
        {
            return module.Delete;
        }
    }
}