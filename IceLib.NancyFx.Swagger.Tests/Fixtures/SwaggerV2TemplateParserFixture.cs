using Xunit;
using System.Collections.Generic;
using RazorEngine;
using RazorEngine.Templating;
using IceLib.NancyFx.Swagger.Models;
using Nancy;
using IceLib.NancyFx.Swagger.Resources;
using IceLib.NancyFx.Swagger.Parser;
using System;
using System.Linq;
using IceLib.NancyFx.Modules;
using Nancy.Responses.Negotiation;
using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Swagger.Attributes;
using IceLib.NancyFx.Extensions;

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

    public class PetsModule : APIModule 
    {
        [Get(Description="Get all pets", Produces = "application/json")]
        [Response(HttpStatusCode.OK, Description = "A list of pets", ReferenceType = typeof(Pet))]
        [Response(HttpStatusCode.Unauthorized, Description = "Access denied")]
        public Negotiator OnGet() 
        {
            return Negotiate
                        .WithModel(new List<Pet>() { new Pet() });
        }
    }

    public class SwaggerV2TemplateParserFixture
    {
        public SwaggerV2TemplateParserFixture()
        {
            
        }

        [Fact]
        public void Should_transfome_model_to_valid_json()
        {
            var swaggerModel = new SwaggerV2()
            {
                Info = InfoMock,
                Paths = new List<PathItem>() { PathMock }
            };

            var result = new SwaggerV2TemplateParser().ParseJSON(swaggerModel);

            Assert.Equal(ExpectedJSON, result);
        }

        [Fact]
        public void Should_create_model_instances_from_attributes() 
        {
            var swagger = new SwaggerV2();

            var methods = new PetsModule().GetActionMethods();

            foreach (var method in methods)
	        {
                var pathItem = new PathItem();

                var httpVerbAttributes = method.GetCustomAttributes(typeof(HttpVerbAttribute), true).FirstOrDefault();

                if (httpVerbAttributes != null)
	            {
                    var verb = (httpVerbAttributes as HttpVerbAttribute);

                    pathItem.Url = verb.ActionPath;

                    var operation = new Operation()
                    {
                        Description = verb.Description,
                        HttpMethod = verb.Method,
                        Produces = verb.Produces
                    };

                    var parameterAttributes = method.GetCustomAttributes(typeof(ParameterAttribute), true);

                    foreach (var paremeter in parameterAttributes)
	                {
                        var parameter = (paremeter as ParameterAttribute);

                        operation.Parameters.Add(parameter.AsModel);
	                }

                    var resposneAttributes = method.GetCustomAttributes(typeof(ResponseAttribute), true);

                    foreach (var operationResponse in resposneAttributes)
	                {
                        var response = (operationResponse as ResponseAttribute);

                        operation.Responses.Add(response.AsModel);
	                }

                    pathItem.Operations.Add(operation);
	            }

                swagger.Paths.Add(pathItem);
	        }

            var _Path = System.Text.Encoding.UTF8.GetString(Templates._Path);

            var pathModel =  swagger.Paths.First();

            var result = Engine.Razor.RunCompile(_Path, "_Path.cshtml", null, pathModel);

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
                        Responses = new List<Models.OperationResponse>()
                        {
                            new Models.OperationResponse()
                            {
                                Description = "A list of pets.",
                                ResponseCode = HttpStatusCode.OK,
                                Schema = new Schema()
                                {
                                    Type = DataType.ReferenceType,
                                    ReferenceType = typeof(Pet[])
                                }
                            },

                            new Models.OperationResponse()
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
@"﻿{
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
