namespace ModernRestApi.Application.Features.UserFeatures.UpdateUser
{
    public sealed record UpdateUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
