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

        public List<User> GetAll()
        {
            return _users.Find(_ => true).ToList();
        }

        public User GetById(string id)
        {
            var user = _users.Find(s => s.Id == id);
            if (user == null || user.CountDocuments() == 0) return null;

            return user.First();
        }


        public void Create(User user)
        {
            try
            {
                _users.InsertOne(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public User Update(User updateduser, string id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            var user = _users.Find(filter).First();

            if (user == null)
                return null;

            user.Name = updateduser.Name;
            user.Password = updateduser.Password;
            user.LastModifiedDate = updateduser.LastModifiedDate;
            _users.ReplaceOneAsync(filter, user);

            return user;


        }

        public void DeleteById(string id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);

            _users.DeleteOneAsync(filter);
        }

        public void DeleteAll()
        {
            _users.DeleteManyAsync(new BsonDocument { });
        }

        public User Login(User user)
        {
            var filter = Builders<User>.Filter.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password));
            IFindFluent<User, User> foundUser = _users.Find(filter);

            if (foundUser == null || foundUser.CountDocuments() == 0) return null;

            return foundUser.First();
        }
    }
}
