using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DNC.Zumo.WebApi.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> SendAsJsonAsync<T>(this HttpClient client, HttpMethod method, string requestUri, T objectValue)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (requestUri == null) throw new ArgumentNullException("requestUri");
            if (objectValue == null) throw new ArgumentNullException("objectValue");

            var serializedObject = JsonConvert.SerializeObject(objectValue, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            var request = new HttpRequestMessage { Method = method, RequestUri = new Uri(client.BaseAddress + "/" + requestUri) };
            request.Content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            return client.SendAsync(request);
        }
    }
}
