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

        public PostAttribute(string actionPath)
            : base(actionPath)
        {

        }

        public override string Method
        {
            get { return "POST"; }
        }
    }
}