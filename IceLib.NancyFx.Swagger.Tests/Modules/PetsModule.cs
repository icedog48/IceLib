using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Modules;
using IceLib.NancyFx.Swagger.Attributes;
using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Tests.Modules
{
    public class PetsModule : APIModule
    {
        public PetsModule()
            : base("/api/v1/pets")
        {

        }

        [Get(Description = "Get all pets", Produces = "application/json")]
        [Response(HttpStatusCode.Unauthorized, Description = "Access denied")]
        [Response(HttpStatusCode.OK, Description = "A list of pets", ReferenceType = typeof(Pet[]))]
        public Negotiator OnGet()
        {
            return Negotiate
                        .WithModel(new List<Pet>() { new Pet() });
        }
    }
}
