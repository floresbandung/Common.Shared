using DD.Tata.Buku.Shared.Http;
using MediatR;

namespace DD.Tata.Buku.Shared.Logs
{
    public class GetLogRequest : IRequest<ApiResult<GetLogResponse>>
    {
        public string ProviderDb { get; set; }
        public string CollectionName { get; set; }
        public string EventId { get; set; }
    }
}