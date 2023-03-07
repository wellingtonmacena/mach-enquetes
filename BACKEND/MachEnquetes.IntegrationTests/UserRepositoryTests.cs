using MachEnquetes.Entities;
using MachEnquetes.Models;
using MachEnquetes.Repositories;
using MachEnquetes.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MachEnquetes.IntegrationTests
{
    public partial class UserRepositoryTests
    {
        public UserRepository UserRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", false, true)
                        .Build();

            var connectionString = config.GetValue<string>($"ConnectionStrings:{nameof(DatabaseSettings.DefaultConnectionTests)}");
            DbContextOptions<MachEnquetesContext>  dbContextOptions = new DbContextOptionsBuilder<MachEnquetesContext>()
                .UseMySQL(connectionString)
                .Options;

            UserRepository = new UserRepository(new MachEnquetesContext(dbContextOptions));
            User user = new User("test", "well@gmail.com", "tstes", "28/04/1998 00:00:00");
            user.Id = 1;
            UserRepository.Create(user);
        }

        [Test]
        public void GetAll_ShouldNotBeEmpty_True()
        {
            var result = UserRepository.GetAll().Result;

            Assert.IsTrue(result.StatusCode == 200 || result.StatusCode == 204);
        }

        [Test]
        [TestCase("1")]
        public void GetById_WhenExists_True(int id)
        {
            var result = UserRepository.GetById(id).Result;

            Assert.IsTrue(result.StatusCode == 200);
        }

        [Test]
        [TestCase("404")]
        public void GetById_WhenExists_False(int id)
        {
            var result = UserRepository.GetById(id).Result;

            Assert.IsTrue(result.StatusCode == 404);
        }

        [Test]
        [TestCase("Wellington", "well2@gmail.com", "tstes")]
        public void InsertOne_WhenAlreadyExists_False(string name, string email, string password)
        {
            var user = new User(name, email, password);

            var result = UserRepository.Create(user);

            Assert.IsTrue(result.Result.StatusCode == 201);
        }

        [Test]
        [TestCase("Wellington", "well@gmail.com", "tstes")]
        public void InsertOne_WhenAlreadyExists_True(string name, string email, string password)
        {
            var user = new User(name, email, password);

            var result = UserRepository.Create(user);

            Assert.IsTrue(result.Result.StatusCode == 406);
        }

        [Test]
        [TestCase("Wellington", "well@gmail.com", "tstes")]
        public void Login_WhenAlreadyExists_True(string name, string email, string password)
        {
            var user = new User(name, email, password);

            var result = UserRepository.Login(user);

            Assert.IsTrue(result.Result.StatusCode == 200);
        }

        [Test]
        [TestCase("Wellington", "well@gmail.com", "<password-word>")]
        public void Login_WhenAlreadyExists_False(string name, string email, string password)
        {
            var user = new User(name, email, password);

            var result = UserRepository.Login(user);

            Assert.IsTrue(result.Result.StatusCode == 404);
        }

        [Test]
        [TestCase(1, "Wellington", "well@gmail.com", "tstes")]
        public void UpdateOne_WhenAlreadyExists_True(int id, string name, string email, string password)
        {
            var user = new User(name, email, password);

            var result = UserRepository.Update(id, user);

            Assert.IsTrue(result.Result.StatusCode == 200);
        }

        [Test]
        [TestCase(1, "Wellington", "well@gmail.com", "tstes")]
        public void UpdateOne_WhenAlreadyExists_False(int id, string name, string email, string password)
        {
            var user = new User(name, email, password);
             UserRepository.DeleteById(id);
            var result = UserRepository.Update(id, user);

            Assert.IsTrue(result.Result.StatusCode == 404);
        }

        [Test]
        public void DeleteAll_WhenIsSuccessful_True()
        {
            var result = UserRepository.DeleteAll();

            Assert.IsTrue(result.Result.StatusCode == 204);
        }

        [Test]
        [TestCase(1)]
        public void DeleteById_WhenExists_True(int id)
        {
            var result = UserRepository.DeleteById(id);

            Assert.IsTrue(result.Result.StatusCode == 200);
        }

        [Test]
        [TestCase(404)]
        public void DeleteById_WhenExists_False(int id)
        {
            var result = UserRepository.DeleteById(id);

            Assert.IsTrue(result.Result.StatusCode == 404);
        }


        [OneTimeTearDown]
        public void DeleteTestsExample()
        {
            UserRepository.DeleteAll();
        }
    }
}