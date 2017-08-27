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

namespace FuzzyCore.Database.Models
{
    public class UserOperations
    {

        private string UserName { get; set; }
        private string Password { get; set; }

        public User Get(string UserName, string Password)
        {
            User failUser = new User();
            failUser.permission = "FAIL";
            try
            {

                this.UserName = UserName;
                this.Password = Password;
                dataBase db = new dataBase();
                var collection = db.monData.GetCollection<User>("Users");
                var list = collection.Find(x => x.name == this.UserName && x.password == this.Password).ToListAsync().Result;
                if (list.Count > 0)
                {
                    User CurrentUser = new User();
                    foreach (var item in list)
                    {
                        CurrentUser = item;
                    }
                    return CurrentUser;
                }
                else
                {
                    return failUser;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return failUser;
            }
        }
    }
}
