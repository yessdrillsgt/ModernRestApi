using AutoMapper;
using MediatR;
using ModernRestApi.Application.Common.Exceptions;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Domain.Common;
using ModernRestApi.Domain.Entities;

namespace ModernRestApi.Application.Features.UserFeatures.UpdateUser
{
    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var existingUser = await _userRepository.GetByName(user.Name, cancellationToken);

            if (existingUser != null)
            {
                existingUser.Address = user.Address;
                _userRepository.Update(existingUser);
                await _unitOfWork.Save(cancellationToken);

                return _mapper.Map<UpdateUserResponse>(existingUser);
            }
            else
            {
                throw new BadRequestException(ValidationErrorMessages.NoUserExists);
            }
        }
    }
}
