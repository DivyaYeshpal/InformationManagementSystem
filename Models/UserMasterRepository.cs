using InformationManagementSystem.App_Start;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationManagementSystem.Models
{
    public class UserMasterRepository :IDisposable
    {
        MongoContext _dbContext;

        [Obsolete]
        public UserMasterRepository()
        {
            _dbContext = new MongoContext();
        }
        public RegisterUser ValidateUser(string username, string password)
        {
            var query = Query.And(Query.EQ("UserName", username), Query.EQ("PassWord", password));
            var collection = _dbContext._database.GetCollection<BsonDocument>("RegisterUser").FindOne(query);
            return _dbContext._database.GetCollection<RegisterUser>("RegisterUser").FindOne(query);
        }
       public void Dispose()
        {

        }
    }
}