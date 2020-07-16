using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationManagementSystem.Models
{
    public class Counter
    {
        [BsonElement("Id")]
        public ObjectId Id { get; set; }
        [BsonElement("seqValue")]
        public string seqValue { get; set; }
    }
}