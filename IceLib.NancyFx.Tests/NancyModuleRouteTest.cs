using Nancy;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using IceLib.NancyFx;
using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Extensions;

namespace IceLib.NancyFx.Tests
{
    public class NancyModuleRouteTest
    {
        public NancyModuleRouteTest()
        {

        }
        
        public Browser Browser
        {
            get
            {
                var bootstrapper = new DefaultNancyBootstrapper();
                
                return new Browser(with =>
                {
                    with.Module<TestModule>();
                });
            }
        }

        [Fact]
        public void Should_return_status_ok_when_get_existing_route()
        {  
            // When
            var result = Browser.Get("/tests", with => {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_return_status_ok_when_get_element_existing_route()
        {
            // When
            var result = Browser.Get("/tests/10", with => {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_return_status_ok_when_post_existing_route()
        {
            // When
            var result = Browser.Post("/tests", with => {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        public class TestModule : NancyModule
        {
            public TestModule() : base("/tests")
            {
                this.BindRoutes();
            }

            [Post]
            public void Create() { }

            [Get]
            public void Retrieve() { }

            [Get("{id}")]
            public void Retrieve(int id)
            {

            }
        }
    }
}
