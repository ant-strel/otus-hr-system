using FluentValidation;
using HR_Portal_API.Models.Request;

namespace HR_Portal_API.Validations;

public class JobRequestValidator : AbstractValidator<JobRequest>
{
    public JobRequestValidator()
    {
        RuleFor(job => job.Description)
            .NotEmpty()
            .WithMessage("Незаполненное поле!")
            .Length(4, 100)
            .WithMessage("Текст должен быть от 4 до 100 символов!");

        RuleFor(job => job.Name)
            .NotEmpty()
            .WithMessage("Незаполненное поле!")
            .Length(4, 100)
            .WithMessage("Текст должен быть от 4 до 100 символов!");
    }
}

public class JobFullRequestValidator : AbstractValidator<JobFullRequest>
{
    public JobFullRequestValidator()
    {
        RuleFor(job => job.Id)
            .NotEmpty()
            .WithMessage("GUID обязателен!");

        RuleFor(job => job.Description)
            .NotEmpty()
            .WithMessage("Незаполненное поле!")
            .Length(4, 100)
            .WithMessage("Текст должен быть от 4 до 100 символов!");

        RuleFor(job => job.Name)
            .NotEmpty()
            .WithMessage("Незаполненное поле!")
            .Length(4, 100)
            .WithMessage("Текст должен быть от 4 до 100 символов!");
    }
}