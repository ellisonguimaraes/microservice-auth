using FluentValidation;
using MicroserviceAuth.Domain.DTO.Application;

namespace MicroserviceAuth.Domain.Validators.Application;

public class ApplicationRequestValidator : AbstractValidator<ApplicationRequest>
{
    public ApplicationRequestValidator()
    {
        RuleFor(ar => ar.Id)
            .NotEmpty()
            .NotNull();

        RuleFor(ar => ar.ApplicationName)
            .NotEmpty()
            .NotNull();

        RuleFor(ar => ar.ApiKey)
            .NotEmpty()
            .NotNull();
    }
}
