using FluentValidation;
using ToDoListApi.Application.DTOs;

namespace ToDoListApi.Application.Validators;

public class UpdateToDoItemValidator : AbstractValidator<UpdateToDoItemDto>
{
    public UpdateToDoItemValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El titulo es obligatorio.")
            .MaximumLength(40).WithMessage("El titulo no puede exceder 40 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("La descripcion no puede exceder 200 caracteres.");

        RuleFor(x => x.MaxCompletionDate)
            .NotEmpty().WithMessage("La fecha maxima de completado es obligatoria.")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("La fecha maxima de completado debe ser hoy o una fecha futura.");
    }
}
