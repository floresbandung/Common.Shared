using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DD.Tata.Buku.Shared.Fault;

namespace DD.Tata.Buku.Shared.Http
{
    public class HttpService : IHttpService
    {

        private readonly HttpClient _client;
        public event EventHandler<AfterResponseEventArgs> AfterResponseEventHandler;
        public event EventHandler<OnErrorEventArgs> OnErrorEventHandler;

        public HttpService()
        {
            _client = new HttpClient();
        }
        
        public virtual async Task<HttpServiceResult<T>> GetAsJson<T>(Uri uri, Action<HttpRequestMessage> action = null) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                action?.Invoke(request);

                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var g = JsonConvert.DeserializeObject<T>(result);
                    return HttpServiceResult<T>.Ok(g, (int)response.StatusCode);
                }

                var failedJson = JsonConvert.DeserializeObject<ErrorResponse>(result);

                return failedJson != null
                    ? HttpServiceResult<T>.Fail(failedJson.ErrorDescription, failedJson.ErrorCode.ToString(), (int)response.StatusCode)
                    : HttpServiceResult<T>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
            }
            catch(Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public virtual async Task<HttpServiceResult<T>> GetAsXml<T>(Uri uri, Action<HttpRequestMessage> action = null) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                action?.Invoke(request);

                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var serializer = new XmlSerializer(typeof(T));
                    T deserialize;
                    using (TextReader reader = new StringReader(result))
                    {
                        deserialize = (T)serializer.Deserialize(reader);
                    }
                    return HttpServiceResult<T>.Ok(deserialize, (int)response.StatusCode);
                }

                var failedJson = JsonConvert.DeserializeObject<ErrorResponse>(result);

                return failedJson != null
                    ? HttpServiceResult<T>.Fail(failedJson.ErrorDescription, failedJson.ErrorCode.ToString(), (int)response.StatusCode)
                    : HttpServiceResult<T>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public virtual async Task<HttpServiceResult<T>> PostAsFormData<T>(Uri uri, List<KeyValuePair<string, string>> keyValues, Action<HttpRequestMessage> action = null) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, uri)
                {
                    Content = new FormUrlEncodedContent(keyValues)
                };

                action?.Invoke(request);

                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return HttpServiceResult<T>.Ok(JsonConvert.DeserializeObject<T>(result), (int)response.StatusCode);
                }

                var failedJson = JsonConvert.DeserializeObject<ErrorResponse>(result);

                return failedJson != null
                    ? HttpServiceResult<T>.Fail(failedJson.ErrorDescription, failedJson.ErrorCode.ToString(), (int)response.StatusCode)
                    : HttpServiceResult<T>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public virtual async Task<HttpServiceResult<T>> PostAsJson<T>(Uri uri, T resource, Action<HttpRequestMessage> action = null) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, uri);

                action?.Invoke(request);

                request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return HttpServiceResult<T>.Ok(JsonConvert.DeserializeObject<T>(result), (int)response.StatusCode);
                }

                var failedJson = JsonConvert.DeserializeObject<ErrorResponse>(result);

                return failedJson != null
                    ? HttpServiceResult<T>.Fail(failedJson.ErrorDescription, failedJson.ErrorCode.ToString(), (int)response.StatusCode)
                    : HttpServiceResult<T>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public virtual async Task<HttpServiceResult<T>> PostAsJson<T>(Uri uri, string resource, Action<HttpRequestMessage> action = null) where T: class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, uri);

                action?.Invoke(request);

                request.Content = new StringContent(resource, Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return HttpServiceResult<T>.Ok(JsonConvert.DeserializeObject<T>(result), (int)response.StatusCode);
                }

                var failedJson = JsonConvert.DeserializeObject<ErrorResponse>(result);

                return failedJson != null
                    ? HttpServiceResult<T>.Fail(failedJson.ErrorDescription, failedJson.ErrorCode.ToString(), (int)response.StatusCode)
                    : HttpServiceResult<T>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public virtual async Task<HttpServiceResult<T>> Post<T>(Uri uri, Action<HttpRequestMessage> action = null) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, uri);

                action?.Invoke(request);

                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var resultSerialized = await response.Content.ReadAsStringAsync();

                return !response.IsSuccessStatusCode ?
                    HttpServiceResult<T>.Fail($"Error occurred while performing post to {uri}: {response} - {resultSerialized}", null, (int)response.StatusCode) :
                    HttpServiceResult<T>.Ok(JsonConvert.DeserializeObject<T>(resultSerialized), (int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public virtual async Task<HttpServiceResult> Put(Uri uri, Action<HttpRequestMessage> action = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, uri);

                action?.Invoke(request);

                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var resultSerialized = await response.Content.ReadAsStringAsync();
                return !response.IsSuccessStatusCode ?
                    HttpServiceResult.Fail($"Error occurred while performing put to {uri}: {response} - {resultSerialized}", null, (int)response.StatusCode) :
                    HttpServiceResult.Ok((int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public virtual async Task<HttpServiceResult<TResponse>> Put<TResponse>(Uri uri, Action<HttpRequestMessage> action = null) where TResponse : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, uri);

                action?.Invoke(request);

                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var resultSerialized = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode ?
                    HttpServiceResult<TResponse>.Ok(JsonConvert.DeserializeObject<TResponse>(resultSerialized), (int)response.StatusCode) :
                    HttpServiceResult<TResponse>.Fail($"Error occurred while performing put to {uri} : {response}", null, (int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public virtual async Task<HttpServiceResult> PutAsJson<T>(Uri uri, T resource, Action<HttpRequestMessage> action = null) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, uri);

                action?.Invoke(request);

                var content = JsonConvert.SerializeObject(resource);
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var resultSerialized = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return HttpServiceResult.Ok((int) response.StatusCode);
                var deserializeObject = JsonConvert.DeserializeObject<ErrorResponse>(resultSerialized);
                return HttpServiceResult.Fail(deserializeObject.ErrorDescription, deserializeObject.ErrorCode.ToString(), (int)deserializeObject.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public async Task<HttpServiceResult<string>> GetAsString(Uri uri, Action<HttpRequestMessage> action = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                action?.Invoke(request);

                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return HttpServiceResult<string>.Ok(result, (int)response.StatusCode);
                }

                var failedJson = JsonConvert.DeserializeObject<ErrorResponse>(result);

                return failedJson != null
                    ? HttpServiceResult<string>.Fail(failedJson.ErrorDescription, failedJson.ErrorCode.ToString(), (int)response.StatusCode)
                    : HttpServiceResult<string>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        public void OnAfterResponseEventHandler(AfterResponseEventArgs e)
        {
            var handler = AfterResponseEventHandler;
            handler?.Invoke(this, e);
        }

        protected void ErrorEventHandler(OnErrorEventArgs e)
        {
            var handler = OnErrorEventHandler;
            handler?.Invoke(this, e);
        }
    }
}