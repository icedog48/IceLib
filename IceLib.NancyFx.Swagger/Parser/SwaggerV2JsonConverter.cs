using IceLib.NancyFx.Swagger.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Parser
{
    public class SwaggerV2JsonConverter : JsonConverter
    {
        #region JsonConverter

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SwaggerV2);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                var swagger = value as SwaggerV2;

                JObject o = (JObject)t;

                foreach (var pathItem in swagger.Paths)
                {
                    var pathItemProperty = new JProperty(pathItem.Url);

                    foreach (var operation in pathItem.Operations)
                    {
                        var responses = new JObject();   

                        foreach (var operationResponse in operation.Responses)
                        {
                            var operationResponseProperty = new JProperty(operationResponse.ResponseCodeAsInt.ToString());

                            var operationResponseObj = new JObject();
                                operationResponseObj.Add("description", operationResponse.Description);

                            if (operationResponse.Schema != null && operationResponse.Schema.TypeAsInt != 0)
                            {
                                var schema = new JObject();
                                
                                if (operationResponse.Schema.Type == DataType.ReferenceType)
                                {
                                    if (operationResponse.Schema.ReferenceType.IsArray)
                                    {
                                        schema.Add("type", "array");
                                    }

                                    var schemaItem = new JObject();
                                        schemaItem.Add("$ref", operationResponse.Schema.ReferenceTypeAsString);

                                        schema.Add("items", schemaItem);    
                                }
                                else
                                {
                                    schema.Add("type", operationResponse.Schema.TypeAsString);
                                }

                                operationResponseObj.Add("schema", schema);
                            }

                            operationResponseProperty.Value = operationResponseObj;

                            responses.Add(operationResponseProperty);
                        }

                        var operationObj = new JObject();
                            operationObj.Add("description", operation.Description);
                            operationObj.Add("produces", new JArray(operation.Produces));
                            operationObj.Add("responses", responses);

                        var operationProperty = new JProperty(operation.HttpMethod);
                            operationProperty.Value = operationObj;

                        pathItemProperty.Value = new JObject(operationProperty);
                    }

                    var pathItemObj = new JObject(pathItemProperty);

                    o.Property("paths").Value = pathItemObj;
                }
                
                o.WriteTo(writer);
            }
        }

        #endregion JsonConverter

        public static string SerializeObject(SwaggerV2 swagger) 
        {
            return JsonConvert.SerializeObject(swagger, Formatting.Indented, new SwaggerV2JsonConverter());
        }
    }
}
