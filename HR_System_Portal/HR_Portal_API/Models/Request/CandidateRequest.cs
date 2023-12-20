using Domain.Entities.Entities;

namespace HR_Portal_API.Models.Request
{
    public record CandidateRequest(
        string LastName,
        string FirstName,
        string Surname,
        int Age,
        string Address);

    public record CandidateFullRequest(
        Guid Id,
        string LastName,
        string FirstName,
        string Surname,
        int Age,
        string Address);
}
