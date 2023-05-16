using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTestWithWebApi.Controllers;
using UnitTestWithWebApi.Models;
using UnitTestWithWebApi.Services;
using xUnitTest.Fixtures;

namespace xUnitTest.Systems.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.
                Setup(service => service.GetAllUsers()).
                ReturnsAsync(UsersFixture.GetTestUsers());

            var sut = new UserController(mockUserService.Object);

            //Act
            var result = (OkObjectResult)await sut.Get();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSucess_InvokeUserServiceExactlyOnce()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
           
            mockUserService.
                Setup(service => service.
                GetAllUsers()).ReturnsAsync(new List<User>());
            
            var sut = new UserController(mockUserService.Object);

            //Act
            var result = sut.Get();

            //Assert
            mockUserService.
                Verify(service => service.GetAllUsers(), 
                Times.Once());


        }

        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();

            mockUserService.
                Setup(service => service.
                GetAllUsers()).ReturnsAsync(UsersFixture.GetTestUsers());
                

            var sut = new UserController(mockUserService.Object);


            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();

        }

        [Fact]
        public async Task Get_OnUsersFound_Retuns404()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();

            mockUserService.
                Setup(service => service.
                GetAllUsers()).ReturnsAsync(new List<User>());

            var sut = new UserController(mockUserService.Object);


            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

    }
}