using Nancy;

namespace IceLib.NancyFx.Binding
{
    public interface IBindHelper<TResource, TModel> where TModel : new()
    {
        TModel BindModel(NancyModule module);

        TResource BindResource(NancyModule module);

        TResource BindResource(TModel model);
    }
}