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
    public class BindHelper<TResource, TModel> : IBindHelper<TResource, TModel>
        where TResource : new()
        where TModel : new()
    {
        private readonly ValidationHelper validationHelper;
        private readonly IMapper<TResource, TModel> resourceMapper;
        private readonly IMapper<TModel, TResource> modelMapper;

        public BindHelper(ValidationHelper validationHelper,
                          IMapper<TResource, TModel> resourceMapper,
                          IMapper<TModel, TResource> modelMapper)
        {
            this.validationHelper = validationHelper;
            this.resourceMapper = resourceMapper;
            this.modelMapper = modelMapper;
        }

        public TResource BindResource(NancyModule module)
        {
            var viewModel = module.Bind<TResource>();

            this.validationHelper.Validate(viewModel);

            return viewModel;
        }

        public TResource BindResource(TModel model)
        {
            return modelMapper.Map(model);
        }

        public TModel BindModel(NancyModule module)
        {
            var viewModel = module.Bind<TResource>();

            this.validationHelper.Validate(viewModel);

            return this.resourceMapper.Map(viewModel);
        }
    }
}
