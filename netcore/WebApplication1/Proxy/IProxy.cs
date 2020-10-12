using ConsumeWebApi.Proxy;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumeWebApi.Proxy
{
    public interface IProxy
    {
        Task<ProxyResponse<T>> DeleteMessageAsync<T>(string uripart, T formData);
        Task<ProxyResponse<T>> GetMessageAsync<T>(string uripart);
        Task<ProxyResponse<T>> PostMessageAsync<T>(string uripart, dynamic formData);
        Task<ProxyResponse<T>> PutMessageAsync<T>(string uripart, T formData);
    }
}