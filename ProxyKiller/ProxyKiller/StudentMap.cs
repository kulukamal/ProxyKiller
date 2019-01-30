using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyKiller
{
    class StudentMap
    {
        [BsonId]
        public string UserName { get; set; }
        public ObjectId PersonId { get; set; }
        
    }
}
