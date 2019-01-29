using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyKiller
{
    class RequestInfo
    {
        [BsonId]
        public string UserNameSubjectId { get; set; }
        public string UserName { get; set; }
        public string SubjectId { get; set; }
    }
}
