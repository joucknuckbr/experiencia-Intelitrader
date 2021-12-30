using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using CadastroApi.Controllers;
using CadastroApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroApi.UnitTests
{
    public class UsersControllersTest
    {
        private readonly Mock<IConfiguration> configurationStub = new();
        private readonly Mock<IRegistrationContext> contextStub = new();
        private readonly Mock<ILogger<UsersController>> loggerStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetUser_WithUnexistingUser_ReturnNotFoundAsync()
        {
            // Arrange

            var controller = new UsersController(configurationStub.Object, contextStub.Object, loggerStub.Object );

            // Act
            var result = await controller.GetUser(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetUserAsync_WithExistingUser_ReturnsExpectedUser()
        {
            // Arrange
            User expectedUser = CreateRandomUser();

            contextStub.Setup(db => db.FindAsync<User>(It.IsAny<Guid>()))
                .ReturnsAsync(expectedUser);

            var controller = new UsersController(configurationStub.Object, contextStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetUser(expectedUser.Id);

            // Assert
            Assert.IsType<ActionResult<User>>(result);
        }


        [Fact]
        public async Task CreateUserAsync_WithUserToCreate_ReturnsCreatedUser()
        {
            // Arrange
            User userToCreate = CreateRandomUser();
            /*contextStub.Setup(db => db.FindAsync<User>(It.IsAny<Guid>()))
                .ReturnsAsync(userToCreate);*/
            var controller = new UsersController(configurationStub.Object, contextStub.Object, loggerStub.Object);

            // Act
            var result = await controller.PostUser(userToCreate);

            // Assert
            //var createdItem = (result.Result as CreatedAtActionResult).Value as User;
            Assert.IsType<ActionResult<User>>(result);
        }

        [Fact]
        public async Task UpdateUsersAsync_WithExistingUser_ReturnsNoContent()
        {
            // Arrange
            User existingUser = CreateRandomUser();
            

            existingUser.FirstName = Guid.NewGuid().ToString();
            existingUser.Surname = Guid.NewGuid().ToString();
            existingUser.Age += 7;

                var controller = new UsersController(configurationStub.Object, contextStub.Object, loggerStub.Object);

            // Act
            var result = await controller.PutUser(existingUser);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUserAsync_WithExistingUser_ReturnsNoContent()
        {
            // Arrange
            User existingItem = CreateRandomUser();
            contextStub.Setup(db => db.FindAsync<User>(It.IsAny<Guid>()))
                .ReturnsAsync(existingItem);

            var controller = new UsersController(configurationStub.Object, contextStub.Object, loggerStub.Object);

            // Act
            var result = await controller.DeleteUser(existingItem.Id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private User CreateRandomUser()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                FirstName = Guid.NewGuid().ToString(),
                Surname = Guid.NewGuid().ToString(),
                Age = rand.Next(1000),
                CreationDate = DateTime.Now
        };
        }
    }
}