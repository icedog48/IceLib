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

        public GetAttribute(string actionPath)
            : base(actionPath)
        {

        }

        public override string Method
        {
            get { return "GET"; }
        }
    }
}