using MediatR;

namespace ModernRestApi.Application.Features.UserFeatures.DeleteUser
{
    public sealed record DeleteUserRequest(string Name) : IRequest<DeleteUserResponse>;
}
