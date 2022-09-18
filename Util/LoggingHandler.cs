using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace APITests.Util
{
    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var toPrint = $"{request.Method} {request.RequestUri}";
            System.Diagnostics.Trace.WriteLine(toPrint);
            if (request.Content != null)
            {
                System.Diagnostics.Trace.WriteLine(await request.Content.ReadAsStringAsync());
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                var @object = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                var formatted = Newtonsoft.Json.JsonConvert.SerializeObject(@object, Newtonsoft.Json.Formatting.Indented);
                System.Diagnostics.Trace.WriteLine(formatted);
            }

            return response;
        }
    }
}
