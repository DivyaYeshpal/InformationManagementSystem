using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace InformationManagementSystem.App_Start
{
    public class MongoContext
    {
        MongoClient _client;
        MongoServer _server;
        public MongoDatabase _database;

        [Obsolete]
        public MongoContext()        //constructor   
        {
            // Reading credentials from Web.config file   
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];  
            //var MongoUsername = ConfigurationManager.AppSettings["MongoUsername"];  
            //var MongoPassword = ConfigurationManager.AppSettings["MongoPassword"]; 
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"];   
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];   

            // Creating credentials  
            //var credential = MongoCredential.CreateMongoCRCredential
                            //(MongoDatabaseName,
                            // MongoUsername,
                            // MongoPassword);

            // Creating MongoClientSettings  
            var settings = new MongoClientSettings
            {
                //Credentials = new[] { credential },
                Server = new MongoServerAddress(MongoHost, Convert.ToInt32(MongoPort))
            };
            _client = new MongoClient(settings);
            _server = _client.GetServer();
            _database = _server.GetDatabase(MongoDatabaseName);
        }
    }
}