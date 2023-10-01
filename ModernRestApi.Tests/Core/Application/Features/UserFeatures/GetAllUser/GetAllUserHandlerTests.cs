using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernRestApi.Application.Features.UserFeatures.GetAllUser;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading;

namespace ModernRestApi.Tests.Core.Application.Features.UserFeatures.GetAllUser
{
    [TestClass]
    public class GetAllUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IMapper> _mapper;
        private GetAllUserHandler _getAllUserHandler;

        public GetAllUserHandlerTests()
        {
            _mapper = new Mock<IMapper>();
            _userRepository = new Mock<IUserRepository>();

            _getAllUserHandler = new GetAllUserHandler(_userRepository.Object, _mapper.Object);
        }

        [TestMethod]
        public void Handle_Test()
        {
            List<User> existingUsers = new List<User>
            {
                new User { Name = "Justin", Address = "123 Main Street" },
                new User { Name = "Natasha", Address = "123 Main Street" }
            };

            List<GetAllUserResponse> mappedResponse = new List<GetAllUserResponse>()
            {
                new GetAllUserResponse(){ Name = "Justin", Address = "123 Main Street" },
                new GetAllUserResponse(){ Name = "Natasha", Address = "123 Main Street" }
            };

            _userRepository.Setup(x => x.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(existingUsers);
            _mapper.Setup(m => m.Map<List<GetAllUserResponse>>(existingUsers)).Returns(mappedResponse);

            var response = _getAllUserHandler.Handle(new GetAllUserRequest(), new CancellationToken());

            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Result.Count);
            Assert.IsTrue(response.Result[0].Name == "Justin");
            Assert.IsTrue(response.Result[1].Name == "Natasha");
        }
    }
}
