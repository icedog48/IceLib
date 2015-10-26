using Nancy;

namespace IceLib.NancyFx.Binding
{
    public interface IBindHelper<TViewModel, TModel> where TModel : new()
    {
        TModel BindValidWith(NancyModule module);
    }
}