using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestWithWebApi.Config;
using UnitTestWithWebApi.Models;
using UnitTestWithWebApi.Services;
using xUnitTest.Fixtures;
using xUnitTest.Helpers;

namespace xUnitTest.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.
                SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            
            var endpoint = "http://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);


            //Act
            await sut.GetAllUsers();

            //Assert
            //Verify HTTP request is made 

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {

            //Arrange
           
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "http://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            //Verify HTTP request is made 

            result.Count.Should().Be(0);

        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {

            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.
                SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "http://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            //Verify HTTP request is made 
            //result.Count.Should().Be(0);
            result.Count.Should().Be(expectedResponse.Count);

        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeConfiguredExternalUrl()
        {

            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "http://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.
                SetupBasicGetResourceList(expectedResponse, endpoint);

            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(
                   req => req.Method == HttpMethod.Get && 
                   req.RequestUri.ToString() == endpoint),
               ItExpr.IsAny<CancellationToken>()
               );

        }
    }
}
