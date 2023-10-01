using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernRestApi.Application.Features.UserFeatures.DeleteUser;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Domain.Common;
using ModernRestApi.Domain.Entities;
using Moq;
using System.Threading;

namespace ModernRestApi.Tests.Core.Application.Features.UserFeatures.DeleteUser
{
    [TestClass]
    public class DeleteUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;
        private DeleteUserHandler _deleteUserHandler;

        public DeleteUserHandlerTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();

            _deleteUserHandler = new DeleteUserHandler(_unitOfWork.Object, _userRepository.Object, _mapper.Object);
        }

        [TestMethod]
        public void Handle_Existing_User_Test()
        {
            User existingUser = new User
            {
                Name = "Existing Valid Name",
                Address = "123 Valid Address"
            };

            DeleteUserRequest request = new DeleteUserRequest(Name: existingUser.Name);
            _mapper.Setup(m => m.Map<User>(request)).Returns(existingUser);

            _userRepository.Setup(x => x.GetByName(request.Name, It.IsAny<CancellationToken>())).ReturnsAsync(existingUser);

            DeleteUserResponse response = (_deleteUserHandler.Handle(request, new CancellationToken())).Result;

            _userRepository.Verify(x => x.Delete(existingUser), Times.Once);
            _unitOfWork.Verify(x => x.Save(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Handle_Non_Existing_User_Test()
        {
            User nonExistingUser = new User
            {
                Name = "Non Existing Valid Name",
                Address = "123 Valid Address"
            };

            DeleteUserRequest request = new DeleteUserRequest(Name: nonExistingUser.Name);
            _mapper.Setup(m => m.Map<User>(request)).Returns(nonExistingUser);

            _userRepository.Setup(x => x.GetByName(request.Name, It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            var response = (_deleteUserHandler.Handle(request, new CancellationToken()));

            _userRepository.Verify(x => x.Delete(nonExistingUser), Times.Never);
            _unitOfWork.Verify(x => x.Save(It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(ValidationErrorMessages.NoUserExists, response.Exception?.InnerExceptions[0].Message);
        }
    }
}
