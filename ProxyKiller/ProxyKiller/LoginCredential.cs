using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyKiller
{
    internal class LoginCredential
    {
        [BsonId]
        public string UserName { get; set; }
        public Int64 Password { get; set; }
        public string Type { get; set; }
    }
}
