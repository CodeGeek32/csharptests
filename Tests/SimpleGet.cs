using APITests.Util;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace APITests
{
    public class SimpleGet
    {
        [SetUp]
        public void SetUp()
        {
            // this code is necessary for disabling SSL certificate error while calling GET on a URL
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        [Test]
        public void SimpleGETApiCall()
        {
            
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.BaseAddress = new System.Uri("https://catfact.ninja/");

            var requestString = "fact";

            var getTask = client.GetStringAsync(requestString);
            getTask.Wait();
        }
    }
}
