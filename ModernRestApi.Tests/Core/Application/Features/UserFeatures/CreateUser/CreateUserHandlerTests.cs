using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernRestApi.Application.Features.UserFeatures.CreateUser;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Domain.Common;
using ModernRestApi.Domain.Entities;
using Moq;
using System.Threading;


namespace ModernRestApi.Tests.Core.Application.Features.UserFeatures.CreateUser
{
    [TestClass]
    public class CreateUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;
        private CreateUserHandler _createUserHandler;

        public CreateUserHandlerTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();

            _createUserHandler = new CreateUserHandler(_unitOfWork.Object, _userRepository.Object, _mapper.Object);
        }

        [TestMethod]
        public void Handle_Unique_User_Test()
        {
            User uniqueUser = new User
            {
                Name = "Unique Valid Name",
                Address = "123 Valid Address"
            };

            CreateUserRequest request = new CreateUserRequest(Name: uniqueUser.Name, Address: uniqueUser.Address);
            _mapper.Setup(m => m.Map<User>(request)).Returns(uniqueUser);

            _userRepository.Setup(x => x.GetByName(request.Name, It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            CreateUserResponse response = (_createUserHandler.Handle(request, new CancellationToken())).Result;

            _userRepository.Verify(x => x.Create(uniqueUser), Times.Once);
            _unitOfWork.Verify(x => x.Save(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Handle_Duplicate_User_Test()
        {
            User duplicateUser = new User
            {
                Name = "Duplicate Valid Name",
                Address = "123 Valid Address"
            };

            CreateUserRequest request = new CreateUserRequest(Name: duplicateUser.Name, Address: duplicateUser.Address);
            _mapper.Setup(m => m.Map<User>(request)).Returns(duplicateUser);
            _userRepository.Setup(x => x.GetByName(request.Name, It.IsAny<CancellationToken>())).ReturnsAsync(duplicateUser);

            var response = _createUserHandler.Handle(request, new CancellationToken());

            _userRepository.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            _unitOfWork.Verify(x => x.Save(It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(ValidationErrorMessages.NoDuplicatesAllowed, response.Exception?.InnerExceptions[0].Message);
        }
    }
}
