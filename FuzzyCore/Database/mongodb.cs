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
using FuzzyCore.Server;

namespace FuzzyCore.Database
{
    public class mongodb
    {
        ConsoleMessage Message = new ConsoleMessage();
        const string defaultHostName = "mongodb://localhost:27017";
        MongoClient monClient;
        bool MongoInıt = false;
        public IMongoDatabase monData;
        public mongodb(string Database)
        {
            try
            {
                monClient = new MongoClient(defaultHostName);
                monData = monClient.GetDatabase(Database);
                bool isMongoLive = monData.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);

                if (isMongoLive)
                {
                    Message.Write("Connected MongoDB!", ConsoleMessage.MessageType.SUCCESS);
                    MongoInıt = true;
                }
                else
                {
                    Message.Write("Not Connected MongoDB!", ConsoleMessage.MessageType.ERROR);
                    MongoInıt = false;
                }
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message.ToString(),ConsoleMessage.MessageType.ERROR);
            }
        }
        public mongodb(string Database,string Host , string UserName , string Password)
        {
            try
            {
                monClient = new MongoClient(Host);
                monData = monClient.GetDatabase(Database);
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
            }
        }
        public mongodb(string Database , string Host)
        {
            try
            {
                monClient = new MongoClient(Host);
                monData = monClient.GetDatabase(Database);
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
            }
        }
    }
}
