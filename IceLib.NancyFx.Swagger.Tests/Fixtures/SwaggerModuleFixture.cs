using Owin;
using Nancy;
using Microsoft.Owin.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IceLib.NancyFx.Swagger.Tests.Fixtures
{
    public class SwaggerModuleFixture
    {
        private TestServer testServer;

        public SwaggerModuleFixture()
        {
            testServer = TestServer.Create(app => 
            {
                app.UseNancy();
            });
        }

        [Fact]
        public void Should_generate_swagger_json_from_end_point() 
        {
            var result = testServer.HttpClient.GetStringAsync("/api/v1/swagger").Result;

            Assert.Equal(string.Empty, result.ToString());
        }
    }
}
