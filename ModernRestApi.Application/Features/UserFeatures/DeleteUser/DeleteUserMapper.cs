using AutoMapper;
using ModernRestApi.Domain.Entities;

namespace ModernRestApi.Application.Features.UserFeatures.DeleteUser
{
    public sealed class DeleteUserMapper : Profile
    {
        public DeleteUserMapper()
        {
            CreateMap<DeleteUserRequest, User>();
            CreateMap<User, DeleteUserResponse>();
        }
    }
}
