using DD.Tata.Buku.Shared.Fault;
using Microsoft.AspNetCore.Mvc;

namespace DD.Tata.Buku.Shared.Http
{
    public static class ActionResultMapper
    {
        public static IActionResult ToActionResult(ApiResult result)
        {
            return result.IsSuccessful
                ? new ObjectResult(null)
                    { StatusCode = result.HttpStatusCode }

                : result.ErrorCode == 0
                    ? new ObjectResult(new ErrorResponse { ErrorDescription = result.ErrorDescription })
                        { StatusCode = result.HttpStatusCode }
                    : new ObjectResult(new ErrorResponse { ErrorDescription = result.ErrorDescription, ErrorCode = result.ErrorCode })
                        { StatusCode = result.HttpStatusCode };
        }

        public static IActionResult ToActionResult<TResponse>(ApiResult<TResponse> result)
        {
            return result.IsSuccessful
                ? new ObjectResult(result.Value)
                    { StatusCode = result.HttpStatusCode }

                : result.ErrorCode == 0
                    ? new ObjectResult(new ErrorResponse { ErrorDescription = result.ErrorDescription })
                        { StatusCode = result.HttpStatusCode }
                    : new ObjectResult(new ErrorResponse { ErrorDescription = result.ErrorDescription, ErrorCode = result.ErrorCode })
                        { StatusCode = result.HttpStatusCode };
        }


        public static IActionResult ToActionResult<T>(T result)
        {
            throw new System.NotImplementedException();
        }
    }
}