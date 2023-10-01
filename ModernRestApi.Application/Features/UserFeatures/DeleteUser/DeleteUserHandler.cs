using AutoMapper;
using MediatR;
using ModernRestApi.Application.Common.Exceptions;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Domain.Common;
using ModernRestApi.Domain.Entities;

namespace ModernRestApi.Application.Features.UserFeatures.DeleteUser
{
    public sealed class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var existingUser = await _userRepository.GetByName(user.Name, cancellationToken);

            if (existingUser != null)
            {
                _userRepository.Delete(existingUser);
                await _unitOfWork.Save(cancellationToken);

                return _mapper.Map<DeleteUserResponse>(existingUser);
            }
            else
            {
                throw new BadRequestException(ValidationErrorMessages.NoUserExists);
            }
        }
    }
}
