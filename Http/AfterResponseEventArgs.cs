using System;
using System.Net.Http;

namespace DD.Tata.Buku.Shared.Http
{
    public class AfterResponseEventArgs : EventArgs
    {
        public HttpResponseMessage Response { get; internal set; }
    }
}