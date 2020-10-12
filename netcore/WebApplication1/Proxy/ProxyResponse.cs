using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumeWebApi.Proxy
{
    public class ProxyResponse<T>
    {
        public HttpResponseMessage ResponseMessage { get; set; }

        private T _value;
        public T GetData { get {
                                if (ResponseMessage.IsSuccessStatusCode) {
                                    var Respose = ResponseMessage.Content.ReadAsStringAsync().Result;
                                    _value = JsonConvert.DeserializeObject<T>(Respose);
                                }
                
                
                                return _value; 
                         }
        }

    }
}
