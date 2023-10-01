using MediatR;

namespace ModernRestApi.Application.Features.UserFeatures.GetAllUser
{
    public sealed record GetAllUserRequest : IRequest<List<GetAllUserResponse>>;
}
