using MediatR;

namespace ModernRestApi.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserRequest(string Name, string Address) : IRequest<CreateUserResponse>;
}
