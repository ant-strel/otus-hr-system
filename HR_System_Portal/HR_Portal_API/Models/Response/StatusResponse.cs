namespace HR_Portal_API.Models.Response
{
    public class StatusResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEnded { get; set; }
        public bool IsActual { get; set; }
    }
}
