using FluentValidation;
using MediatR;
using Security.Common.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Application.SeedWork
{
    public class RequestValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var r = request;
            var validationFailures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .ToList();

            if (validationFailures.Any())
            {
                List<ErrorCode> lstCustomErrorException = new List<ErrorCode>();
                foreach (var item in validationFailures)
                {
                    ErrorCode objErrorCode = null;
                    if (item.ErrorMessage.Contains('|'))
                    {
                        string[] lstErrorCode = item.ErrorMessage.Split('|');
                        string strCode = lstErrorCode[0];
                        string description = lstErrorCode[1];

                        EnumErrorCode code = (EnumErrorCode)Enum.Parse(typeof(EnumErrorCode), strCode);
                        objErrorCode = FactoryErrorCode.GetErrorCode(code);
                        objErrorCode.Description = description;
                    }
                    else
                    {
                        objErrorCode = FactoryErrorCode.GetErrorCode(EnumErrorCode.Generic);
                        objErrorCode.Description = item.ErrorMessage;
                    }
                    lstCustomErrorException.Add(objErrorCode);
                }
                throw new CustomErrorException(lstCustomErrorException);
            }

            return next();
        }
    }
}
