using Nancy;
using System;
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

        public DeleteAttribute(string actionPath)
            : base(actionPath)
        {

        }

        public override string Method
        {
            get { return "DELETE"; }
        }
    }
}