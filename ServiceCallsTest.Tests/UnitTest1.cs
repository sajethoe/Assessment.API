using Assessment.API.Controllers;
using Assessment.API.Data;
using Assessment.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ServiceCallsTest.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var dbContextInMemory = await GetDatabaseContext();
            var controller = new ServiceCallController(dbContextInMemory);

            var calls = await controller.GetServiceCalls();

            Assert.NotNull(calls);
        }

        [Fact]
        public async Task GetById_ReturnOk()
        {
            var dbContextInMemory = await GetDatabaseContext();
            var controller = new ServiceCallController(dbContextInMemory);
            ServiceCall testServiceCall = await dbContextInMemory.ServiceCalls.FindAsync(2);

            var result = await controller.GetById(2) as ObjectResult;
            var actualResult = result.Value;

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);

            Assert.Equal(testServiceCall.Title, ((ServiceCall)actualResult).Title);

        }

        [Fact]
        public async Task GetById_ReturnNotNull()
        {
            var dbContextInMemory = await GetDatabaseContext();
            var controller = new ServiceCallController(dbContextInMemory);

            var result = await controller.GetById(2);
            
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateServiceCall_IfIdNotSame_ReturnBadRequest()
        {
            int idWrong = 1;
            int idSc = 2;

            var dbContextInMemory = await GetDatabaseContext();
            var controller = new ServiceCallController(dbContextInMemory);
            var testServiceCall = await controller.GetById(idSc); // system null reference

            var result = await controller.UpdateServiceCall(idWrong, (ServiceCall)testServiceCall);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(idWrong, notFoundObjectResult.Value);

        }


        private async Task<ServiceCallDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ServiceCallDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ServiceCallDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.ServiceCalls.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.ServiceCalls.Add(new ServiceCall()
                    {
                        Id = i,
                        Title = $"testServiceCall{i}",
                        Description = $"testDescription{i}",
                        CreatedOn = DateTime.UtcNow,
                        Status = 0
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}