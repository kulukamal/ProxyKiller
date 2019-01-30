using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProxyKiller
{
    class StudentAttendance
    {
        [BsonId]
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int Absent { get; set; }
        public List<DateTime> listOfAbsents { get; set; }
    }
}
