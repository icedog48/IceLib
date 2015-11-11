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

        public PutAttribute(string actionPath)
            : base(actionPath)
        {

        }

        public override string Method
        {
            get { return "PUT"; }
        }
    }
}