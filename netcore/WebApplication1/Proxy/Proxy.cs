
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeWebApi.Proxy
{
    public class Proxy : IProxy, IDisposable
    {

        private readonly string _baseurl = "";
        private readonly string _encode = "";

        private HttpClient _client;

        public Proxy(string baseurl, string encode)
        {
            _baseurl = baseurl;
            _encode = encode;

            _client = new HttpClient();

            _client.BaseAddress = new Uri(_baseurl);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_encode));


        }


        //public HttpClient getClientBase()
        //{

        //    _client.BaseAddress = new Uri(_baseurl);
        //    _client.DefaultRequestHeaders.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_encode));

        //    return _client;

        //}

        public async Task<ProxyResponse<T>> GetMessageAsync<T>(string uripart)
        {

            var Response = await _client.GetAsync(uripart);

            var proxyresponse = new ProxyResponse<T>() { ResponseMessage = Response };

            return proxyresponse;

        }

        public async Task<ProxyResponse<T>> PostMessageAsync<T>(string uripart,dynamic formData)
        {

            var keyvalues = ObjectExtensions.ToKeyValue(formData);

            var content = new FormUrlEncodedContent(keyvalues);

            var Response = await _client.PostAsync(uripart, content);

            var proxyresponse = new ProxyResponse<T>() { ResponseMessage = Response };

            return proxyresponse;
        }


        public async Task<ProxyResponse<T>> PutMessageAsync<T>(string uripart, T formData)
        {

            return null;
        }

        public async Task<ProxyResponse<T>> DeleteMessageAsync<T>(string uripart, T formData)
        {



            return null;
        }

        public void Dispose()
        {
           if (!(_client == null)) _client.Dispose();
        }
    }

}
