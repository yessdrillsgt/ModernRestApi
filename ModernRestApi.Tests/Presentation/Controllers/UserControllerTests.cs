using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernRestApi.Application.Features.UserFeatures.CreateUser;
using ModernRestApi.Application.Features.UserFeatures.DeleteUser;
using ModernRestApi.Application.Features.UserFeatures.GetAllUser;
using ModernRestApi.Application.Features.UserFeatures.UpdateUser;
using ModernRestApi.WebApi.Controllers;
using Moq;
using System.Collections.Generic;

namespace ModernRestApi.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _userController = new UserController(_mediator.Object);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Create_Verify_Send_TimesOnce()
        {
            CreateUserRequest testRequest = new CreateUserRequest("user name", "user address");

            _mediator.Setup(x => x.Send(testRequest, It.IsAny<System.Threading.CancellationToken>()))
                .ReturnsAsync(new CreateUserResponse());

            var result = _userController.Create(testRequest, new System.Threading.CancellationToken());

            Assert.IsNotNull(result);
            _mediator.Verify(x => x.Send(testRequest, It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Update_Verify_Send_TimesOnce()
        {
            UpdateUserRequest testRequest = new UpdateUserRequest("user name", "update user address");

            _mediator.Setup(x => x.Send(testRequest, It.IsAny<System.Threading.CancellationToken>()))
                .ReturnsAsync(new UpdateUserResponse());

            var result = _userController.Update(testRequest, new System.Threading.CancellationToken());

            Assert.IsNotNull(result);
            _mediator.Verify(x => x.Send(testRequest, It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Delete_Verify_Send_TimesOnce()
        {
            DeleteUserRequest testRequest = new DeleteUserRequest("user name");

            _mediator.Setup(x => x.Send(testRequest, It.IsAny<System.Threading.CancellationToken>()))
                .ReturnsAsync(new DeleteUserResponse());
            var result = _userController.Delete(testRequest, new System.Threading.CancellationToken());

            Assert.IsNotNull(result);
            _mediator.Verify(x => x.Send(testRequest, It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAll_Verify_Send_TimesOnce()
        {
            GetAllUserRequest testRequest = new GetAllUserRequest();

            _mediator.Setup(x => x.Send(testRequest, It.IsAny<System.Threading.CancellationToken>()))
                .ReturnsAsync(new List<GetAllUserResponse>());

            var result = _userController.GetAll(new System.Threading.CancellationToken());

            Assert.IsNotNull(result);
            _mediator.Verify(x => x.Send(testRequest, It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }
    }
}