//using MachEnquetes.Models;
//using MachEnquetes.Repositories;
//using MachEnquetes.Utils;
//using Microsoft.Extensions.Configuration;

//namespace MachEnquetes.IntegrationTests
//{
//    public class UserRepositoryTests
//    {
//        public UserRepository UserRepository { get; set; }
//        [SetUp]
//        public void Setup()
//        {
//            var config = new ConfigurationBuilder()
//        .SetBasePath(AppContext.BaseDirectory)
//        .AddJsonFile("appsettings.json", false, true)
//        .Build();

//            var connectionString = config.GetValue<string>($"DatabaseSettings:{nameof(DatabaseSettings.ConnectionString)}");
//            var databaseNameHomol = config.GetValue<string>($"DatabaseSettings:{nameof(DatabaseSettings.DatabaseNameHomol)}");
//            var userCollectionName = config.GetValue<string>($"DatabaseSettings:{nameof(DatabaseSettings.UserCollectionName)}");

//            UserRepository = new UserRepository(connectionString, databaseNameHomol, userCollectionName);
//            UserRepository.Create(new User("test") { Email = "well@gmail.com", Password = "tstes" });
//        }

//        [Test]
//        public void GetAll_ShouldNotBeEmpty_True()
//        {
//            var result = UserRepository.GetAll();

//            Assert.IsTrue(result.StatusCode == 200 || result.StatusCode == 204);
//        }

//        [Test]
//        [TestCase("test")]
//        public void GetById_WhenExists_True(string id)
//        {
//            var result = UserRepository.GetById(id);

//            Assert.IsTrue(result.StatusCode == 200);
//        }

//        [Test]
//        [TestCase("nonExistent")]
//        public void GetById_WhenExists_False(string id)
//        {
//            var result = UserRepository.GetById(id);

//            Assert.IsTrue(result.StatusCode == 404);
//        }


//        [Test]
//        [TestCase("Wellington", "well2@gmail.com", "tstes")]
//        public void InsertOne_WhenAlreadyExists_False(string name, string email, string password)
//        {
//            var user = new User(name, email, password);

//            var result = UserRepository.Create(user);

//            Assert.IsTrue(result.StatusCode == 201);
//        }

//        [Test]
//        [TestCase("Wellington", "well@gmail.com", "tstes")]
//        public void InsertOne_WhenAlreadyExists_True(string name, string email, string password)
//        {
//            var user = new User(name, email, password);

//            var result = UserRepository.Create(user);

//            Assert.IsTrue(result.StatusCode == 406);
//        }

//        [Test]
//        [TestCase("Wellington", "well@gmail.com", "tstes")]
//        public void Login_WhenAlreadyExists_True(string name, string email, string password)
//        {
//            var user = new User(name, email, password);

//            var result = UserRepository.Login(user);

//            Assert.IsTrue(result.StatusCode == 200);
//        }

//        [Test]
//        [TestCase("Wellington", "well@gmail.com", "<password-word>")]
//        public void Login_WhenAlreadyExists_False(string name, string email, string password)
//        {
//            var user = new User(name, email, password);

//            var result = UserRepository.Login(user);

//            Assert.IsTrue(result.StatusCode == 404);
//        }

//        [Test]
//        [TestCase("test", "Wellington", "well@gmail.com", "tstes")]
//        public void UpdateOne_WhenAlreadyExists_True(string id, string name, string email, string password)
//        {
//            var user = new User(name, email, password);

//            var result = UserRepository.Update(id, user);

//            Assert.IsTrue(result.StatusCode == 200);
//        }

//        [Test]
//        [TestCase("test1", "Wellington", "well@gmail.com", "tstes")]
//        public void UpdateOne_WhenAlreadyExists_False(string id, string name, string email, string password)
//        {
//            var user = new User(name, email, password);

//            var result = UserRepository.Update(id, user);

//            Assert.IsTrue(result.StatusCode == 404);
//        }

//        [Test]
//        public void DeleteAll_WhenIsSuccessful_True()
//        {
//            var result = UserRepository.DeleteAll();

//            Assert.IsTrue(result.StatusCode == 204);
//        }

//        [Test]
//        [TestCase("test")]
//        public void DeleteById_WhenExists_True(string id)
//        {
//            var result = UserRepository.DeleteById(id);

//            Assert.IsTrue(result.StatusCode == 200);
//        }

//        [Test]
//        [TestCase("test1")]
//        public void DeleteById_WhenExists_False(string id)
//        {
//            var result = UserRepository.DeleteById(id);

//            Assert.IsTrue(result.StatusCode == 404);
//        }


//        [Test]
//        [OneTimeTearDown]
//        public void DeleteTestsExample()
//        {
//            UserRepository.DeleteAll();
//        }
//    }
//}