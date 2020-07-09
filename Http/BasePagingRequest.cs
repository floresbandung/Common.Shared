namespace DD.Tata.Buku.Shared.Http
{
    public class BasePagingRequest
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string Sort { get; set; }
        public string Direction { get; set; }
        public int GetSkip()
        {
            if ((Page - 1) < 0) return 0;
            return (Page - 1) * Limit;
        }
    }
}
