using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Bson;

namespace FuzzyCore.Database
{
    public class dataBase
    {
        MongoClient monClient = new MongoClient("mongodb://localhost:27017");
        public IMongoDatabase monData;
        public dataBase() { monData = monClient.GetDatabase("NetworkApp"); }
    }
}
