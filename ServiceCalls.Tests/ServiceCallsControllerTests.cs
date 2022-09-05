using Assessment.API.Controllers;
using Xunit;
using FakeItEasy;
using Assessment.API.Data;
using Moq;
using Assessment.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ServiceCalls.Tests
{
    public class ServiceCallsControllerTests
    {
        [Fact]
        public async Task GetById_IfNull_ReturnNotFound()
        {
            // Arrange
            var respositoryStub = new Mock<ServiceCallDbContext>();
            respositoryStub.Setup(repo => repo.ServiceCalls.FindAsync(It.IsAny<int>()))
                .ReturnsAsync((ServiceCall)null);

            var controller = new ServiceCallController(respositoryStub.Object);

            // Act
            Random random = new Random();
            int testId = random.Next(0, 100);
            var result = await controller.GetById(testId);

            // Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}