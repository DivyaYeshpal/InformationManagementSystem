using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationManagementSystem.Models
{
    public class RegisterUser
    {
        [BsonId]
        public Double _id { get; set; }
        public Double Id 
        { get {return Double.Parse(_id.ToString()); }
            set { _id = Double.Parse(value.ToString()); } }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("EmailiD")]
        public string EmailiD { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("PassWord")]
        public string PassWord { get; set; }
        [BsonElement("UserRole")]
        public string UserRole { get; set; }
    }
}