using Xunit;
using System.Collections.Generic;
using RazorEngine;
using RazorEngine.Templating;
using IceLib.NancyFx.Swagger.Models;
using Nancy;
using IceLib.NancyFx.Swagger.Resources;

namespace IceLib.NancyFx.Swagger.Tests
{
    public class Pet
    {
        public string Name { get; set; }
    }

    public class ErrorMessage
    {
        public string FieldName { get; set; }

        public string Message { get; set; }
    }

    public class Class1
    {
        public Class1()
        {
            
        }

        [Fact]
        public void Fixture()
        {
            var swaggerModel = new SwaggerV2()
            {
                Info = InfoMock,
                Paths = new List<PathItem>() { PathMock }
            };

            var Swagger = System.Text.Encoding.UTF8.GetString(Templates.Swagger);

            var _Info = System.Text.Encoding.UTF8.GetString(Templates._Info);

            var _Path = System.Text.Encoding.UTF8.GetString(Templates._Path);

            var razorService = Engine.Razor;
                razorService.AddTemplate("_Path", _Path);
                razorService.AddTemplate("_Info", _Info);
                razorService.AddTemplate("Swagger", Swagger);

            var result = Engine.Razor.RunCompile(Swagger, "Swagger.cshtml", null, swaggerModel);

            Assert.Equal(ExpectedJSON, result);
        }

        public PathItem PathMock
        {
            get
            {
                return new PathItem()
                {
                    #region path

                    Url = "/pets",
                    Operations = new List<Operation>()
                {
                    new Operation()
                    {
                        HttpMethod = "GET",
                        Description = "Returns all pets from the system that the user has access to",
                        Produces = "application/json",
                        Responses = new List<Models.Response>()
                        {
                            new Models.Response()
                            {
                                Description = "A list of pets.",
                                ResponseCode = HttpStatusCode.OK,
                                Schema = new Schema()
                                {
                                    Type = DataType.ReferenceType,
                                    ReferenceType = typeof(Pet[])
                                }
                            },

                            new Models.Response()
                            {
                                Description = "Acess error.",
                                ResponseCode = HttpStatusCode.Unauthorized,
                                Schema = new Schema()
                                {
                                    Type = DataType.String
                                }
                            }
                        }
                    }
                }

                    #endregion path
                };
            }
        }

        public APIInfo InfoMock
        {
            get
            {
                return new APIInfo()
                {
                    Title = "Pet Store API",
                    Description = "Pet Store API",
                    TermsOfService = string.Empty,
                    Contact = new Contact(),
                    License = new License(),
                    Version = "1.0"
                };
            }
        }

        public string ExpectedJSON
        {
            get
            {
                return
@"﻿﻿{
	""swagger"": ""2.0"",

    ﻿    ""info"": {
        ""description"": ""Pet Store API"",
        ""version"": ""1.0"",
        ""title"": ""Pet Store API"",
        ""termsOfService"": """",

        ""contact"": {
            ""email"": """"
        },

        ""license"": {
            ""name"": """",
            ""url"": """"
        }
    }, 

﻿    ""/pets"" : {
         ""GET"" : {
              ""description"" : ""Returns all pets from the system that the user has access to"",
              ""produces"": [ ""application/json"" ],
              ""responses"": {
                  ""200"" : {
                      ""description"" : ""A list of pets."",
                      ""schema"" : {
                          ""type"": ""array"",
                          ""$ref"": ""#/definitions/Pet""
                      }
                  }
                  ""401"" : {
                      ""description"" : ""Acess error."",
                      ""schema"" : {
                          ""type"": String
                      }
                  }
              }
         }
    } , 
}";
            }
        }
    }
}
