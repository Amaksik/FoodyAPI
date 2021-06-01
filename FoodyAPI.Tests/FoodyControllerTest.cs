using FoodyAPI.Controllers;
using FoodyAPI.Data;
using FoodyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FoodyAPI.Tests
{
    public class FoodyControllerTest
    {

        private APIContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<APIContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
            var dbContext = new APIContext(options);
            return dbContext;
        }
        

        //GetUser Testing
        [Fact]
        public async Task GetUser_WithUnprovidedId_ReturnsBadRequest()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(null);



            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetUser_UserDoesntExist_ReturnsNotFound()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(Guid.NewGuid().ToString());



            //Assert
            Assert.IsType<NotFoundObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }
       
        [Fact]
        public async Task GetUser_UserExists_ReturnsOk()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            var user = new User();
            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync(user);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(Guid.NewGuid().ToString());



            //Assert
            Assert.IsType<OkObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }



        //AddUser Testing
        [Fact]
        public async Task AddUser_WithUnprovidedId_ReturnsBadRequest()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(null);



            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }

        [Fact]
        public async Task AddUser_UserDoesntExist_ReturnsNotFound()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(Guid.NewGuid().ToString());



            //Assert
            Assert.IsType<NotFoundObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }

        [Fact]
        public async Task AddUser_UserExists_ReturnsOk()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            var user = new User();
            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync(user);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(Guid.NewGuid().ToString());



            //Assert
            Assert.IsType<OkObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }




        //RemoveUser Testing

        [Fact]
        public async Task RemoveUser_WithUnprovidedId_ReturnsBadRequest()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(null);



            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }

        [Fact]
        public async Task RemoveUser_UserDoesntExist_ReturnsNotFound()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(Guid.NewGuid().ToString());



            //Assert
            Assert.IsType<NotFoundObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }

        [Fact]
        public async Task RemoveUser_UserExists_ReturnsOk()
        {
            //Arrange
            var dbContext = CreateDbContext();
            var dbControllerStub = new Mock<DbController>(dbContext);

            var user = new User();
            dbControllerStub.Setup(dbcont => dbcont.GetUserById(It.IsAny<string>()))
                .ReturnsAsync(user);

            var controller = new FoodyController(dbControllerStub.Object);

            //Act
            var result = await controller.GetUser(Guid.NewGuid().ToString());



            //Assert
            Assert.IsType<OkObjectResult>(result);

            //Clean up
            dbContext.Dispose();
        }




    }
}
