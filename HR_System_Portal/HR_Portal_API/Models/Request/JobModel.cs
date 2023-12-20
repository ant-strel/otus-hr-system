namespace HR_Portal_API.Models.Request
{
    public record JobRequest(
        string Name,
        string Description);

    public record JobFullRequest(
        Guid Id,
        string Name,
        string Description);
}
