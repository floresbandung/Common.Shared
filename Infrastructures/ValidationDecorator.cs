using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DD.Tata.Buku.Shared.Fault;
using DD.TataBuku.Shared.Fault;
using MediatR;

namespace DD.Tata.Buku.Shared.Infrastructures
{
    public class ValidationDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IRequestValidator<TRequest>> _validators;

        public ValidationDecorator(IEnumerable<IRequestValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any()) return await next();
            foreach (var validator in _validators.OrderBy(v => v.Order))
            {
                var result = await validator.Validate(request);

                if (result.IsFailure)
                    throw new ApiException(result.HttpStatusCode, result.ErrorDescription, result.ErrorCode, result.EventId);
            }

            return await next();
        }
    }
}
