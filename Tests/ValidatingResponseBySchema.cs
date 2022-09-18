using APITests.Util;
using NUnit.Framework;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace APITests
{
    public class GetValidateBySchema
    {
        string requestString = "fact";

        HttpClient client;
        string schemaFileName = "get_cat_fact_schema.json";
        JSchema schema;
        string extraFieldsSchemaFileName = "get_cat_fact_extra_fields_schema.json";
        JSchema expectExtraFieldsSchema;

        [OneTimeSetUp]
        public void SetUp()
        {
            client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.BaseAddress = new System.Uri("https://catfact.ninja/");

            var stringSchema = File.ReadAllText(ProjectSourcePath.Value + $"/schemas/{schemaFileName}");
            schema = JSchema.Parse(stringSchema);

            var extraFields = File.ReadAllText(ProjectSourcePath.Value + $"/schemas/{extraFieldsSchemaFileName}");
            expectExtraFieldsSchema = JSchema.Parse(extraFields);
        }

        [Test]
        public void ValidateResponse()
        {
            var getTask = client.GetStringAsync(requestString);
            getTask.Wait();

            JObject cat = JObject.Parse(getTask.Result);

            IList<string> messages;
            bool valid = cat.IsValid(schema, out messages);
            if (!valid)
            {
                System.Diagnostics.Trace.WriteLine(messages);
            }
        }

        [Test]
        public void WillFailValidateWithWrongSchema()
        {
            var getTask = client.GetStringAsync(requestString);
            getTask.Wait();

            JObject cat = JObject.Parse(getTask.Result);

            IList<string> messages;
            bool valid = cat.IsValid(expectExtraFieldsSchema, out messages);
            if (!valid)
            {
                foreach (var message in messages)
                {
                    System.Diagnostics.Trace.WriteLine(message);
                }
                Assert.Fail($"Failed to validate against schema \"{extraFieldsSchemaFileName}\"");
            }
        }
    }
}
