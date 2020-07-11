using System.Collections.Generic;
using DD.Tata.Buku.Common.Shared.Logs;

namespace DD.Tata.Buku.Shared.Logs
{
    public class GetLogResponse
    {
        public List<LogRequest> List { get; set; }
    }
}