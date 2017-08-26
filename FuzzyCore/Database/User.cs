using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Database.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string premission { get; set; }
    }
}
