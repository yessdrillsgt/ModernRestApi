namespace ModernRestApi.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
