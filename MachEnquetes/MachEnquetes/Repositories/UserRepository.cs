using MachEnquetes.Models;
using MachEnquetes.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MachEnquetes.Repositories
{
    public class UserRepository : Controller
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<DatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _users = mongoClient
                .GetDatabase(options.Value.DatabaseName)
                .GetCollection<User>(options.Value.UserCollectionName);
        }

        public UserRepository(string connectionString, string databaseName, string userCollectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            _users = mongoClient
                .GetDatabase(databaseName)
                .GetCollection<User>(userCollectionName);
        }

        public ObjectResult GetAll()
        {
            try
            {
                var objects = _users.Find(_ => true).ToList();
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
                var user = _users.Find(s => s.Id == id);
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
                var user = _users.Find(s => s.Email == email);
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
                    _users.InsertOne(user);
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
                var foundUser = _users.Find(filter);

                if (foundUser == null || foundUser.CountDocuments() == 0)
                    return StatusCode(404, null);

                var user = foundUser.First();
                user.FullName = updatedUser.FullName;
                user.Password = updatedUser.Password;
                user.LastModifiedDate = updatedUser.LastModifiedDate;
                _users.ReplaceOneAsync(filter, user);

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

                    _users.DeleteOneAsync(filter);

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
                _users.DeleteManyAsync(new BsonDocument { });
                return StatusCode(204, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public ObjectResult Login(User user)
        {
            var filter = Builders<User>.Filter.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password));
            IFindFluent<User, User> foundUser = _users.Find(filter);

            if (foundUser == null || foundUser.CountDocuments() == 0)
                return StatusCode(404, user);

            return StatusCode(200, user);
        }
    }
}
