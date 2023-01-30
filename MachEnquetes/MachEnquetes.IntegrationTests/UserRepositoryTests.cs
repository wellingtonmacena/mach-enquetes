using MachEnquetes.Models;
using MachEnquetes.Repositories;
using MachEnquetes.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace MachEnquetes.IntegrationTests
{
    public class UserRepositoryTests
    {
        public UserRepository UserRepository { get; set; }
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", false, true)
        .Build();

            var connectionString = config.GetValue<string>($"DatabaseSettings:{nameof(DatabaseSettings.ConnectionString)}");
            var databaseNameHomol = config.GetValue<string>($"DatabaseSettings:{nameof(DatabaseSettings.DatabaseNameHomol)}");
            var userCollectionName = config.GetValue<string>($"DatabaseSettings:{nameof(DatabaseSettings.UserCollectionName)}");

            UserRepository = new UserRepository(connectionString, databaseNameHomol, userCollectionName);
            UserRepository.Create(new User("test") { Email= "well@gmail.com" });
        }

        [Test]
        public void GetAll_ShouldNotBeEmpty_True()
        {
            var result = UserRepository.GetAll();

            Assert.IsTrue(result.StatusCode == 200 || result.StatusCode == 204);
        }

        [Test]
        [TestCase("test")]
        public void GetById_WhenExists_True(string id)
        {
            var result = UserRepository.GetById(id);

            Assert.IsTrue(result.StatusCode == 200);
        }

        [Test]
        [TestCase("nonExistent")]
        public void GetById_WhenExists_False(string id)
        {
            var result = UserRepository.GetById(id);

            Assert.IsTrue(result.StatusCode == 404);
        }


        [Test]
        [TestCase("Wellington", "well2@gmail.com", "tstes")]
        public void InsertOne_WhenAlreadyExists_False(string name,string email, string password)
        {
            var user = new User(name, email, password);

            var result = UserRepository.Create(user);

            Assert.IsTrue(result.StatusCode == 200);
        }

        [Test]
        [TestCase("Wellington", "well@gmail.com", "tstes")]
        public void InsertOne_WhenAlreadyExists_True(string name, string email, string password)
        {
            var user = new User(name, email, password);

            var result = UserRepository.Create(user);

            Assert.IsTrue(result.StatusCode == 406);
        }

        [Test]
        [OneTimeTearDown]
        public void DeleteTestsExample()
        {
            UserRepository.DeleteAll();
        }

    }
}