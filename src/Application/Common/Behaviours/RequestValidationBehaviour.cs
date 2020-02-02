using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using System.Linq;

namespace Application.Common.Behaviours
{
    public class RequestValidationBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            var context = new ValidationContext(request);

            var errors = _validators
                .Select(x => x.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(x => x != null)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ValidationException(errors);
            }

            return next();

        }
    }
}
