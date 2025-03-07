using FluentValidation;

namespace ToDoList.Common.DTOs;

public class FilterGetTaskDtoValidator : AbstractValidator<FilterGetTaskDto>
{
    public FilterGetTaskDtoValidator()
    {
        RuleFor(x => x)
            .Must(x => !(x.FromDate.HasValue && x.ToDate.HasValue) || x.ToDate > x.FromDate)
            .WithMessage("ToDate must be greater than FromDate when both dates are provided.");

    }
}
