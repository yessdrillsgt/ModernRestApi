using AutoMapper;
using ModernRestApi.Domain.Entities;

namespace ModernRestApi.Application.Features.UserFeatures.GetAllUser
{
    public sealed class GetAllUserMapper : Profile
    {
        public GetAllUserMapper()
        {
            CreateMap<User, GetAllUserResponse>();
        }
    }
}
