using System.Linq.Expressions;
using FluentValidation;
using HR_Portal_API.Models.Request;

namespace HR_Portal_API.Validations;

public class CandidateRequestValidator : AbstractValidator<CandidateRequest>
{
    public CandidateRequestValidator()
    {
        void Rule(Expression<Func<CandidateRequest, string>> expression)
            => RuleFor(expression)
                .NotEmpty()
                .WithMessage("Незаполненное поле!");

        Rule(candidate => candidate.FirstName);
        Rule(candidate => candidate.LastName);
        Rule(candidate => candidate.Surname);
        RuleFor(candidate => candidate.Age)
            .GreaterThan(16)
            .WithMessage("Некорректный возраст!")
            .LessThan(100)
            .WithMessage("Некорректный возраст!");
        Rule(candidate => candidate.Address);
    }
}

public class CandidateFullRequestValidator : AbstractValidator<CandidateFullRequest>
{
    public CandidateFullRequestValidator()
    {
        void Rule(Expression<Func<CandidateFullRequest, string>> expression)
            => RuleFor(expression)
                .NotEmpty()
                .WithMessage("Незаполненное поле!");

        RuleFor(candidate => candidate.Id)
            .NotEmpty()
            .WithMessage("GUID обязателен!");

        Rule(candidate => candidate.FirstName);
        Rule(candidate => candidate.LastName);
        Rule(candidate => candidate.Surname);
        RuleFor(candidate => candidate.Age)
            .GreaterThan(16)
            .WithMessage("Некорректный возраст!")
            .LessThan(100)
            .WithMessage("Некорректный возраст!");
        Rule(candidate => candidate.Address);
    }
}