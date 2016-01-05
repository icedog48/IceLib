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
        public JObject GetSchema(OperationResponse operationResponse)
        {
            var schema = new JObject();

            if (operationResponse.Schema.Type == DataType.ReferenceType)
            {
                if (operationResponse.Schema.ReferenceType.IsArray)
                {
                    schema.Add("type", "array");
                }

                var schemaItem = new JObject();
                schemaItem.Add("$ref", string.Format("#/definitions/{0}", operationResponse.Schema.ReferenceTypeAsString));

                schema.Add("items", schemaItem);
            }
            else
            {
                schema.Add("type", operationResponse.Schema.TypeAsString);
            }

            return schema;
        }

        public JProperty GetOperationResponseProperty(OperationResponse operationResponse)
        {
            var operationResponseProperty = new JProperty(operationResponse.ResponseCodeAsInt.ToString());

            var operationResponseObj = new JObject();
            operationResponseObj.Add("description", operationResponse.Description);

            var schemaHasDataType = operationResponse.Schema != null && operationResponse.Schema.TypeAsInt != 0;

            if (schemaHasDataType)
            {
                operationResponseObj.Add("schema", this.GetSchema(operationResponse));
            }

            operationResponseProperty.Value = operationResponseObj;

            return operationResponseProperty;
        }

        public JProperty GetOperationProperty(Operation operation)
        {
            var responses = new JObject();

            foreach (var operationResponse in operation.Responses)
            {
                responses.Add(GetOperationResponseProperty(operationResponse));
            }

            var operationObj = new JObject();
                operationObj.Add("description", operation.Description);
                operationObj.Add("produces", new JArray(operation.Produces));
                operationObj.Add("responses", responses);

            var operationProperty = new JProperty(operation.HttpMethod.ToLower());
                operationProperty.Value = operationObj;

            return operationProperty;
        }

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
            JToken valueToken = JToken.FromObject(value);

            if (valueToken.Type != JTokenType.Object)
            {
                valueToken.WriteTo(writer);
            }
            else
            {
                var swaggerV2 = value as SwaggerV2;

                var valueObject = (JObject)valueToken;

                foreach (var pathItem in swaggerV2.Paths)
                {
                    var pathItemProperty = new JProperty(pathItem.Url);

                    foreach (var operation in pathItem.Operations)
                    {
                        pathItemProperty.Value = new JObject(GetOperationProperty(operation));
                    }

                    var pathItemObj = new JObject(pathItemProperty);

                    valueObject.Property("paths").Value = pathItemObj;
                }

                var parameterTypes = swaggerV2.Paths
                                                .SelectMany(x => x.Operations)
                                                .SelectMany(x => x.Parameters)
                                                    .Where(x => x.Schema != null && x.Schema.Type == DataType.ReferenceType)
                                                        .Select(x => x.Schema.ReferenceType);

                var responseTypes = swaggerV2.Paths
                                                .SelectMany(x => x.Operations)
                                                .SelectMany(x => x.Responses)
                                                    .Where(x => x.Schema != null && x.Schema.Type == DataType.ReferenceType)
                                                        .Select(x => x.Schema.ReferenceType);

                var definitionTypes = new List<Type>()
                                            .Union(parameterTypes)
                                            .Union(responseTypes);

                var definitionsObject = new JObject();

                foreach (var type in definitionTypes)
                {
                    var propType = type;

                    if (type.IsArray) propType = type.GetElementType();

                    var propertiesObject = new JObject();

                    var propType_Properties = propType.GetProperties();

                    foreach (var typeProperty in propType_Properties)
                    {
                        var typePropertyObject = new JObject();
                            typePropertyObject.Add("type", "string");
                            typePropertyObject.Add("description", "");

                        var typePropertyJSONProperty = new JProperty(typeProperty.Name);
                            typePropertyJSONProperty.Value = typePropertyObject;

                        propertiesObject.Add(typePropertyJSONProperty);
                    }

                    var propertiesProperty = new JProperty("properties");
                        propertiesProperty.Value = propertiesObject;

                    var definitionObject = new JObject();
                        definitionObject.Add("type", "object");
                        definitionObject.Add(propertiesProperty);

                    var definitionProperty = new JProperty(propType.Name);
                        definitionProperty.Value = definitionObject;

                    definitionsObject.Add(definitionProperty);
                }

                valueObject.Property("definitions").Value = definitionsObject;

                valueObject.WriteTo(writer);
            }
        }

        #endregion JsonConverter

        public static string SerializeObject(SwaggerV2 swagger) 
        {
            return JsonConvert.SerializeObject(swagger, Formatting.Indented, new SwaggerV2JsonConverter());
        }
    }
}
