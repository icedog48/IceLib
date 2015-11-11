using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Modules;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace POC.NancyFx.Tests.Fixtures
{
    public class AttributeRouteModuleFixture
    {
        #region NancyModule

        public class PetResourceModel 
        {
            public string Name { get; set; }
        }

        public class PetsModule : APIModule
        {
            public PetsModule()
                : base("/api/v1/pets")
            {

            }

            [Get]
            public Negotiator OnGet()
            {
                var list = new List<PetResourceModel>() { new PetResourceModel() { Name = "Pet" } };

                return this.Negotiate
                                .WithStatusCode(HttpStatusCode.OK)
                                .WithModel(list);
            }

            [Get("{id}")]
            public Negotiator OnGet(int id)
            {
                return this.Negotiate
                                .WithStatusCode(HttpStatusCode.OK)
                                .WithModel(new PetResourceModel() { Name = "Pet" });
            }
        }

        #endregion NancyModule

        #region Tests

        [Fact(DisplayName = "Should_return_pet")]
        public void Should_return_pet()
        {
            var bootstrapper = new DefaultNancyBootstrapper();

            var browser = new Browser(bootstrapper);

            // Given, When
            var response = browser.Get("/api/v1/pets/1", (with) =>
            {
                with.HttpRequest();
                with.Accept("application/json");
                with.Header("User-Agent", "Nancy Browser");
                with.FormValue("UserName", "demo");
                with.FormValue("Password", "demo");
            });

            // Then
            Assert.Equal<HttpStatusCode>(HttpStatusCode.OK, response.StatusCode);

            var pet = response.Body.DeserializeJson<PetResourceModel>();            
            Assert.NotNull(pet);
            Assert.Equal("Pet", pet.Name);
        }

        [Fact(DisplayName = "Should_return_pets")]
        public void Should_return_pets()
        {
            var bootstrapper = new DefaultNancyBootstrapper();

            var browser = new Browser(bootstrapper);

            // Given, When
            var response = browser.Get("/api/v1/pets", (with) =>
            {
                with.HttpRequest();
                with.Accept("application/json");
                with.Header("User-Agent", "Nancy Browser");
                with.FormValue("UserName", "demo");
                with.FormValue("Password", "demo");
            });

            var pets = response.Body.DeserializeJson<IEnumerable<PetResourceModel>>();

            // Then
            Assert.NotNull(pets);

            Assert.Equal("Pet", pets.First().Name);
        }

        [Fact(DisplayName = "Should return error from not found route")]
        public void Should_return_error_from_not_found_route()
        {
            var bootstrapper = new DefaultNancyBootstrapper();

            var browser = new Browser(bootstrapper);

            // Given, When
            var response = browser.Get("/api/v1/pets/teste", (with) =>
            {
                with.HttpRequest();
                with.Accept("application/json");
                with.Header("User-Agent", "Nancy Browser");
                with.FormValue("UserName", "demo");
                with.FormValue("Password", "demo");
            });

            // Then
            Assert.Equal<HttpStatusCode>(HttpStatusCode.OK, response.StatusCode);

            var pet = response.Body.DeserializeJson<PetResourceModel>();
            Assert.NotNull(pet);
            Assert.Equal("Pet", pet.Name);
        }

        #endregion Tests
    }
}
