using FluentValidation;
using MediatR.Pipeline;

namespace Demo.Core.Application.Behaviours;

public class ValidationBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<TRequest> _logger;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return;
        }

        var context = new ValidationContext<TRequest>(request);

        var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = results
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (!failures.Any())
        {
            return;
        }

        var name = typeof(TRequest).FullName;
        var errors = string.Join(",", failures.Select(x => x.ErrorMessage));
        _logger.LogInformation("{Name} Failed validation {@Request} {errors}", name, request, errors);
        throw new ValidationException(failures);
    }
}