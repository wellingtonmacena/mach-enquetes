using MachEnquetes.Models;
using MachEnquetes.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MachEnquetes.Repositories
{
    public class SurveyRepository : Controller
    {
        private readonly IMongoCollection<User> _surveys;

        public SurveyRepository(IOptions<DatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _surveys = mongoClient
                .GetDatabase(options.Value.DatabaseName)
                .GetCollection<User>(options.Value.SurveyCollectionName);
        }

        public SurveyRepository(string connectionString, string databaseName, string collectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            _surveys = mongoClient
                .GetDatabase(databaseName)
                .GetCollection<User>(collectionName);
        }

        public ObjectResult GetAll()
        {
            try
            {
                var objects = _surveys.Find(_ => true).ToList();
                if (objects.Count == 0)
                    return StatusCode(204, objects);

                return StatusCode(200, objects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public ObjectResult GetById(string id)
        {
            try
            {
                var user = _surveys.Find(s => s.Id == id);
                if (user == null || user.CountDocuments() == 0)
                    return StatusCode(404, user);

                return StatusCode(200, user.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public ObjectResult GetByEmail(string email)
        {
            try
            {
                var user = _surveys.Find(s => s.Email == email);
                if (user == null || user.CountDocuments() == 0)
                    return StatusCode(404, user);

                return StatusCode(200, user.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public ObjectResult Create(User user)
        {
            try
            {
                var foundUser = GetByEmail(user.Email);
                if (foundUser.StatusCode == 404)
                {
                    _surveys.InsertOne(user);
                    return StatusCode(201, user);
                }

                return StatusCode(406, new { Message = "Already exists " });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public ObjectResult Update(string id, User updatedUser)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);
                var foundUser = _surveys.Find(filter);

                if (foundUser == null || foundUser.CountDocuments() == 0)
                    return StatusCode(404, null);

                var user = foundUser.First();
                user.FullName = updatedUser.FullName;
                user.Password = updatedUser.Password;
                user.LastModifiedDate = updatedUser.LastModifiedDate;
                _surveys.ReplaceOneAsync(filter, user);

                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public ObjectResult DeleteById(string id)
        {
            try
            {
                var foundUser = GetById(id);
                if (foundUser.StatusCode == 200)
                {
                    var filter = Builders<User>.Filter.Eq(x => x.Id, id);

                    _surveys.DeleteOneAsync(filter);

                    return StatusCode(200, null);
                }

                return foundUser;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public ObjectResult DeleteAll()
        {
            try
            {
                _surveys.DeleteManyAsync(new BsonDocument { });
                return StatusCode(204, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        internal object GetAllById(User user)
        {
            throw new NotImplementedException();
        }
    }
}
