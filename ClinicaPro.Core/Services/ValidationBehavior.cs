using MediatR;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// NAMESPACE ALTERADO para ClinicaPro.Core.Services
namespace ClinicaPro.Core.Services
{
    // Esta classe é um "pipeline behavior" do MediatR que roda antes de cada Handler.
    // Ela intercepta a requisição, executa o FluentValidation e lança uma exceção se houver falhas.
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        // Coleção de todos os validadores injetados para o tipo TRequest
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        // Método principal do pipeline.
        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            // Se houver validadores para o Request atual:
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // Executa todos os validadores de forma assíncrona
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                // Filtra apenas os erros
                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                // Se houver erros, lança a exceção de validação
                if (failures.Any())
                {
                    // Lança a exceção de validação que o middleware pode capturar
                    throw new ValidationException(failures);
                }
            }

            // Se a validação passar, passa a requisição para o próximo handler.
            return await next();
        }
    }
}
