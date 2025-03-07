using FluentValidation;

namespace ToDoList.Common.DTOs;

public class AddTaskDtoValidator : AbstractValidator<AddTaskDto>
{
    public AddTaskDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(0, 500).WithMessage("Description must be at most 500 characters.");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.Now).WithMessage("Due date must be in the future.");
    }
}
