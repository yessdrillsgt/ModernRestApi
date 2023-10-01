using MediatR;

namespace ModernRestApi.Application.Features.UserFeatures.UpdateUser
{
    public sealed record UpdateUserRequest(string Name, string Address) : IRequest<UpdateUserResponse>;
}
