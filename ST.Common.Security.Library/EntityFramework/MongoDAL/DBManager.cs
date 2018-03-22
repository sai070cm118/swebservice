using MongoDB.Driver;
using ST.Common.Security.Library.Entities;
using ST.Common.Security.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.EntityFramework.MongoDAL
{
    public class DBManager
    {
        string connectionString = ConfigurationStore.GetMongoConnection();
        string mongoDatabase = ConfigurationStore.GetMongoDatabase();

        MongoClient client = null;
        MongoServer server = null;
        MongoDatabase database = null;

        public DBManager()
        {
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase("users");
            database.DropCollection(mongoDatabase);
        }



        public MongoProfile Add(MongoProfile mongoProfile)
        {

            var collection = database.GetCollection<MongoProfile>("MongoProfile");
            collection.Insert(mongoProfile);

            return mongoProfile;

        }


        
    }
}
