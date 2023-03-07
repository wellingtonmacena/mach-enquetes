using MachEnquetes.Entities;
using MachEnquetes.Models;
using MachEnquetes.Repositories;
using MachEnquetes.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MachEnquetes.IntegrationTests.Repositories
{
    public class SurveyRepositoryTests
    {
        public SurveyRepository SurveyRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", false, true)
                        .Build();

            var connectionString = config.GetValue<string>($"ConnectionStrings:{nameof(DatabaseSettings.DefaultConnectionTests)}");
            DbContextOptions<MachEnquetesContext> dbContextOptions = new DbContextOptionsBuilder<MachEnquetesContext>()
                .UseMySQL(connectionString)
                .Options;

            SurveyRepository = new SurveyRepository(new MachEnquetesContext(dbContextOptions));
            Survey survey = new Survey(1, "Teste", false, true, 1, "30/03/2023 00:00:00");
            SurveyRepository.Create(survey).Wait();
        }

        [Test]
        public void GetAll_ShouldNotBeEmpty_True()
        {
            var result = SurveyRepository.GetAll().Result;

            Assert.IsTrue(result.StatusCode == 200 || result.StatusCode == 204);
        }

        [Test]
        [TestCase(1)]
        public void GetById_WhenExists_True(int id)
        {
            var result = SurveyRepository.GetById(id).Result;

            Assert.IsTrue(result.StatusCode == 200);
        }

        [Test]
        [TestCase("404")]
        public void GetById_WhenExists_False(int id)
        {
            var result = SurveyRepository.GetById(id).Result;

            Assert.IsTrue(result.StatusCode == 404);
        }

        [Test]
        [TestCase(2, "Quem deve ganhar o prêmio", false, true, 1, "30/03/2023 00:00:00")]
        public void CreateOne_WhenAlreadyExists_False(int id, string title, bool canUnregistredVote, bool canUserUpdateVote, int optionsSelectedCount, string finishDate)
        {
            var survey = new Survey(id, title, canUnregistredVote, canUserUpdateVote, optionsSelectedCount, finishDate);

            var result = SurveyRepository.Create(survey);

            Assert.IsTrue(result.Result.StatusCode == 201);
        }

        [Test]
        [TestCase(1, "Quem deve ganhar o prêmio", false, true, 1, "30/03/2023 00:00:00")]
        public void CreateOne_WhenAlreadyExists_True(int id, string title, bool canUnregistredVote, bool canUserUpdateVote, int optionsSelectedCount, string finishDate)
        {
            var survey = new Survey(id, title, canUnregistredVote, canUserUpdateVote, optionsSelectedCount, finishDate);

            var result = SurveyRepository.Create(survey);

            Assert.IsTrue(result.Result.StatusCode == 406);
        }

        [Test]
        [TestCase(1, "Quem deve ganhar o prêmio", false, true, 1, "30/03/2023 00:00:00")]
        public void UpdateOne_WhenAlreadyExists_True(int id, string title, bool canUnregistredVote, bool canUserUpdateVote, int optionsSelectedCount, string finishDate)
        {
            var survey = new Survey(id, title, canUnregistredVote, canUserUpdateVote, optionsSelectedCount, finishDate);

            var result = SurveyRepository.Update(id, survey);

            Assert.IsTrue(result.Result.StatusCode == 200);
        }

        [Test]
        [TestCase(1, "Quem deve ganhar o prêmio", false, true, 1, "30/03/2023 00:00:00")]
        public void UpdateOne_WhenAlreadyExists_False(int id, string title, bool canUnregistredVote, bool canUserUpdateVote, int optionsSelectedCount, string finishDate)
        {
            var survey = new Survey(id, title, canUnregistredVote, canUserUpdateVote, optionsSelectedCount, finishDate);

            SurveyRepository.DeleteById(id);
            var result = SurveyRepository.Update(id, survey);

            Assert.IsTrue(result.Result.StatusCode == 404);
        }

        [Test]
        public void DeleteAll_WhenIsSuccessful_True()
        {
            var result = SurveyRepository.DeleteAll();

            Assert.IsTrue(result.Result.StatusCode == 204);
        }

        [Test]
        [TestCase(1)]
        public void DeleteById_WhenExists_True(int id)
        {
            var result = SurveyRepository.DeleteById(id);

            Assert.IsTrue(result.Result.StatusCode == 200);
        }

        [Test]
        [TestCase(404)]
        public void DeleteById_WhenExists_False(int id)
        {
            var result = SurveyRepository.DeleteById(id);

            Assert.IsTrue(result.Result.StatusCode == 404);
        }


        [OneTimeTearDown]
        public void DeleteTestsExample()
        {
            SurveyRepository.DeleteAll();
        }
    }
}
