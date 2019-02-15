using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace design_dance
{
    class User
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string nama { get; set; }
        public string username { get; set; }
        public string password{ get; set; }
        public string role { get; set; }

    }
}
