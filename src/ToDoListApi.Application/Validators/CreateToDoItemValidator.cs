using FluentValidation;
using ToDoListApi.Application.DTOs;

namespace ToDoListApi.Application.Validators;

public class CreateToDoItemValidator : AbstractValidator<CreateToDoItemDto>
{
    public CreateToDoItemValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El titulo es obligatorio.")
            .MaximumLength(40).WithMessage("El titulo no puede exceder 40 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("La descripcion no puede exceder 200 caracteres.");

        RuleFor(x => x.MaxCompletionDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("La fecha maxima de completado debe ser hoy o una fecha futura.");
    }
}
