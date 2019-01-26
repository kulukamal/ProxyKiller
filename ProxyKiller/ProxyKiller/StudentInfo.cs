using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProxyKiller
{
    class StudentInfo
    {
        [BsonId]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MobileNo { get; set; }
        public string ImageLocation { get; set; }
        public int Absent = 0;
    }
}
