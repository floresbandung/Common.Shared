using System.Net;

namespace DD.Tata.Buku.Shared.Fault
{
    public class ErrorResponse
    {
        public string ErrorDescription { get; set; }
        public int ErrorCode { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}