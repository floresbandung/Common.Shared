namespace DD.Tata.Buku.Shared.Http
{
    public class BaseResponse
    {
        public int ErrorCode { get; set; } = 0;
        public string Description { get; set; } = "Successful";
    }
}
