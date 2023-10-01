using AutoMapper;
using ModernRestApi.Domain.Entities;

namespace ModernRestApi.Application.Features.UserFeatures.UpdateUser
{
    public sealed class UpdateUserMapper : Profile
    {
        public UpdateUserMapper()
        {
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserResponse>();
        }
    }
}
