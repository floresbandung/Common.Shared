using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DD.Tata.Buku.Shared.Http
{
    public interface IHttpService
    {

        Task<HttpServiceResult<T>> GetAsJson<T>(Uri uri, Action<HttpRequestMessage> action = null) where T : class;
        Task<HttpServiceResult<string>> GetAsString(Uri uri, Action<HttpRequestMessage> action = null);
        Task<HttpServiceResult<T>> GetAsXml<T>(Uri uri, Action<HttpRequestMessage> action = null) where T : class;
        
        Task<HttpServiceResult<T>> PostAsFormData<T>(Uri uri, List<KeyValuePair<string, string>> keyValues, Action<HttpRequestMessage> action = null) where T : class;
        
        Task<HttpServiceResult<T>> PostAsJson<T>(Uri uri, T resource, Action<HttpRequestMessage> action = null) where T : class;

        Task<HttpServiceResult<T>> Post<T>(Uri uri, Action<HttpRequestMessage> action = null) where T : class;

        Task<HttpServiceResult> Put(Uri uri, Action<HttpRequestMessage> action = null);
        
        Task<HttpServiceResult<TResponse>> Put<TResponse>(Uri uri, Action<HttpRequestMessage> action = null) where TResponse : class;
        
        Task<HttpServiceResult> PutAsJson<T>(Uri uri, T resource, Action<HttpRequestMessage> action = null) where T : class;

        Task<HttpServiceResult<T>> PostAsJson<T>(Uri uri, string resource, Action<HttpRequestMessage> action = null) where T : class;


    }
}