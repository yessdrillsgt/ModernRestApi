using AutoMapper;
using MediatR;
using ModernRestApi.Application.Common.Exceptions;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Domain.Common;
using ModernRestApi.Domain.Entities;

namespace ModernRestApi.Application.Features.UserFeatures.CreateUser
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var existingUser = await _userRepository.GetByName(user.Name, cancellationToken);

            if (existingUser == null)
            {
                _userRepository.Create(user);
                await _unitOfWork.Save(cancellationToken);

                return _mapper.Map<CreateUserResponse>(user);
            }
            else
            {
                throw new BadRequestException(ValidationErrorMessages.NoDuplicatesAllowed);
            }
        }
    }
}
