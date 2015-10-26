using IceLib.Core.Model.Mapping;
using IceLib.Validation;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Binding
{
    public class BindHelper<TViewModel, TModel> : IBindHelper<TViewModel, TModel>
        where TViewModel : new()
        where TModel : new()
    {
        private readonly ValidationHelper validationHelper;
        private readonly IMapper<TViewModel, TModel> mapper;

        public BindHelper(ValidationHelper validationHelper,
                          IceLib.Core.Model.Mapping.IMapper<TViewModel, TModel> mapper)
        {
            this.validationHelper = validationHelper;
            this.mapper = mapper;
        }

        public TModel BindValidWith(NancyModule module)
        {
            var viewModel = module.Bind<TViewModel>();

            this.validationHelper.Validate(viewModel);

            return this.mapper.Map(viewModel);
        }
    }
}
