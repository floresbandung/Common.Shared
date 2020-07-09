using System;

namespace DD.Tata.Buku.Shared.Http
{
    public class OnErrorEventArgs : EventArgs
    {
        public OnErrorEventArgs(Exception e)
        {
            Exception = e;
        }
        public Exception Exception { get;}
    }
}
