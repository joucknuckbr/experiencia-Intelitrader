using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CadastroApi.Controllers;
using CadastroApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CadastroApi
{
    [TestClass]
    public class TestUsers
    {
        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testUsers = GetTestUsers();

            var controller = new UsersController(testUsers);

            var result = controller.GetUsers();
            Assert.AreEqual(testUsers.Count, result.);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var testUsers = GetTestUsers();
            var controller = new UsersController(testUsers);

            var result = await controller.GetAllProductsAsync() as List<User>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public void GetProduct_ShouldReturnCorrectProduct()
        {
            var testUsers = GetTestUsers();
            var controller = new UsersController(testProducts);

            var result = controller.GetProduct(4) as OkNegotiatedContentResult<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[3].Name, result.Content.Name);
        }

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnCorrectProduct()
        {
            var testUsers = GetTestUsers();
            var controller = new UsersController(testUsers);

            var result = await controller.GetProductAsync(4) as OkNegotiatedContentResult<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testUsers[3].Name, result.Content.Name);
        }

        [TestMethod]
        public void GetProduct_ShouldNotFindProduct()
        {
            var controller = new UsersController(GetTestProducts());

            var result = controller.GetUser(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<User> GetTestUsers()
        {
            var testUsers = new List<User>();
            testUsers.Add(new User { Id = f211fe98 - 5a01 - 4391 - bbb9 - 4924f7342f7d, FirstName = "Nuno", Surname = "Proença", Age = 22, CreationDate = 0001 - 01 - 01T00:00:00 });
            testUsers.Add(new User { Id = 2, Name = "Demo2", Price = 3.75M });
            testUsers.Add(new User { Id = 3, Name = "Demo3", Price = 16.99M });
            testUsers.Add(new User { Id = 4, Name = "Demo4", Price = 11.00M });

            return testUsers;
        }
    }
}