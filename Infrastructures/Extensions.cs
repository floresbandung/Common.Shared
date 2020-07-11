using Microsoft.AspNetCore.Builder;

namespace DD.Tata.Buku.Shared.Infrastructures
{
    public static class Extensions
    {
        public static IApplicationBuilder UseFailureMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DefaultFailureMiddleware>();
        }
    }
}
