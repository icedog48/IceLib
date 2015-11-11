using IceLib.NancyFx.Helpers;
using Nancy;
using Nancy.Responses;
using Nancy.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace IceLib.NancyFx.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public abstract class HttpVerbAttribute : Attribute
    {
        private string actionPath;

        #region Constructors

        public HttpVerbAttribute()
        {

        }

        public HttpVerbAttribute(string actionPath)
        {
            this.actionPath = actionPath;
        }

        #endregion Constructors

        #region Properties

        public string ActionPath
        {
            get
            {
                return actionPath ?? string.Empty;
            }
        }

        public abstract string Method { get; }

        #endregion Properties
    }
}