using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Helpers;
using IceLib.NancyFx.Extensions;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Routing;
using Nancy.Validation;
using Nancy.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IceLib.NancyFx.Modules
{
    public abstract class APIModule : INancyModule
    {
        #region INancyModule

        public AfterPipeline After { get; set; }

        public BeforePipeline Before { get; set; }

        public ErrorPipeline OnError { get; set; }

        public NancyContext Context { get; set; }

        public IResponseFormatter Response { get; set; }

        public IModelBinderLocator ModelBinderLocator { get; set; }

        public ModelValidationResult ModelValidationResult { get; set; }

        public IModelValidatorLocator ValidatorLocator { get; set; }

        public Request Request { get; set; }

        public IViewFactory ViewFactory { get; set; }

        public string ModulePath { get; set; }

        public ViewRenderer View
        {
            get { return new ViewRenderer(this); }
        }

        public Negotiator Negotiate
        {
            get { return new Negotiator(this.Context); }
        }

        public IEnumerable<Route> Routes
        {
            get
            {
                return this.GetRoutes();
            }
        }

        public dynamic Text { get; set; }

        #endregion INancyModule

        public APIModule()
            : this(string.Empty)
        {

        }

        public APIModule(string modulePath)
        {
            this.After = new AfterPipeline();
            this.Before = new BeforePipeline();
            this.OnError = new ErrorPipeline();

            this.ModulePath = modulePath;
        }

        protected virtual IEnumerable<Route> GetRoutes()
        {
            var routes = new List<Route>();

            //Get methods with HttpVerbAttribute
            var handlerMethods = this.GetActionMethods();

            foreach (var method in handlerMethods)
            {
                AddRoutes(routes, method);
            }

            return routes;
        }

        protected virtual void AddRoutes(List<Route> routes, MethodInfo method)
        {
            var httpVerbAttributes = method.GetCustomAttributes(typeof(HttpVerbAttribute), true);

            foreach (var attribute in httpVerbAttributes)
            {
                routes.Add(BindRoute(method, attribute as HttpVerbAttribute));
            }
        }

        protected virtual Route BindRoute(MethodInfo method, HttpVerbAttribute httpVerbAttribute)
        {
            var path = new RouteHelper()
                                .AddPath(this.ModulePath)
                                .AddPath(httpVerbAttribute.ActionPath)
                                    .ToString();

            return Route.FromSync(httpVerbAttribute.Method, path, null, GetSyncFunc(method));
        }

        protected virtual Func<dynamic, dynamic> GetSyncFunc(MethodInfo method)
        {
            return routeParameters =>
            {
                //Try to recover the 'method parameters' from the 'route parameters'
                var parameters = method.GetActionParameters((DynamicDictionary)routeParameters);
                
                if (!method.ReturnType.Equals(typeof(Negotiator)))
                {
                    var errMsg = string.Format("Action method '{0}', must return a Negotiator instance.", method.Name);

                    throw new InvalidOperationException(errMsg);
                }

                try
                {
                    return method.Invoke(this, parameters);
                }
                catch (Exception)
                {
                    var errMsg = string.Format("Error on action '{0}'", method.Name);
                    
                    return Negotiate
                            .WithStatusCode(HttpStatusCode.InternalServerError)
                            .WithModel(errMsg);
                }
            };
        }
    }
}