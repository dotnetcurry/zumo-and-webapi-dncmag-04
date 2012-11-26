using DNC.Zumo.WebApi.Models;
using DNC.Zumo.WebApi.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DNC.Zumo.WebApi.Models
{
    public class ZumoRepository<T> : IRepository<T> where T : Entity
    {
        private HttpClient _client;
        private string _key = "YOUR API KEY";
        private string _address = "https://YOUR SERVICE NAME.azure-mobile.net/tables/" + typeof(T).Name.ToString();

        public ZumoRepository()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
            _client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", _key);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<T> GetAll()
        {
            var resultTask = _client.GetStringAsync(string.Empty);
            var obj = JsonConvert.DeserializeObject<IEnumerable<T>>(resultTask.Result);
            return obj;
        }

        public T Get(int id)
        {
            var resultTask = _client.GetStringAsync("?$filter=Id eq " + id);
            var obj = JsonConvert.DeserializeObject<IEnumerable<T>>(resultTask.Result);
            return obj != null ? obj.FirstOrDefault() : null;
        }

        public void Add(T obj)
        {
            var responseTask = _client.SendAsJsonAsync<T>(HttpMethod.Post,string.Empty,obj);
            if (!responseTask.Result.IsSuccessStatusCode)
                throw new HttpResponseException(responseTask.Result.StatusCode);
        }

        public void Update(T obj)
        {
            var responseTask = _client.SendAsJsonAsync<T>(new HttpMethod("PATCH"), obj.id.ToString(), obj);
            if (!responseTask.Result.IsSuccessStatusCode)
                throw new HttpResponseException(responseTask.Result.StatusCode);
        }

        public void Delete(int id)
        {
            var responseTask = _client.DeleteAsync(_client.BaseAddress+"/"+id.ToString());
            if (!responseTask.Result.IsSuccessStatusCode)
                throw new HttpResponseException(responseTask.Result.StatusCode);
        }
    }
}