using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernRestApi.Application.Features.UserFeatures.UpdateUser;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Domain.Common;
using ModernRestApi.Domain.Entities;
using Moq;
using System.Threading;

namespace ModernRestApi.Tests.Core.Application.Features.UserFeatures.UpdateUser
{
    [TestClass]
    public class UpdateUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;
        private UpdateUserHandler _updateUserHandler;

        public UpdateUserHandlerTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _updateUserHandler = new UpdateUserHandler(_unitOfWork.Object, _userRepository.Object, _mapper.Object);
        }

        [TestMethod]
        public void Handle_Existing_User_Test()
        {
            User existingUser = new User
            {
                Name = "Existing Valid Name",
                Address = "123 Valid Address"
            };

            UpdateUserRequest request = new UpdateUserRequest(Name: existingUser.Name, Address: existingUser.Address);
            _mapper.Setup(m => m.Map<User>(request)).Returns(existingUser);

            _userRepository.Setup(x => x.GetByName(request.Name, It.IsAny<CancellationToken>())).ReturnsAsync(existingUser);

            UpdateUserResponse response = (_updateUserHandler.Handle(request, new CancellationToken())).Result;

            _userRepository.Verify(x => x.Update(existingUser), Times.Once);
            _unitOfWork.Verify(x => x.Save(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Handle_Non_Existing_User_Test()
        {
            User nonExistingUser = new User
            {
                Name = "Non Existing Name",
                Address = "123 Valid Address"
            };

            UpdateUserRequest request = new UpdateUserRequest(Name: nonExistingUser.Name, Address: nonExistingUser.Address);
            _mapper.Setup(m => m.Map<User>(request)).Returns(nonExistingUser);
            _userRepository.Setup(x => x.GetByName(request.Name, It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            var response = _updateUserHandler.Handle(request, new CancellationToken());

            _userRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
            _unitOfWork.Verify(x => x.Save(It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual(ValidationErrorMessages.NoUserExists, response.Exception?.InnerExceptions[0].Message);
        }


    }
}
