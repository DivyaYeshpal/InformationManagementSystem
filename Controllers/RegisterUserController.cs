using InformationManagementSystem.App_Start;
using InformationManagementSystem.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using System;
using System.Web.Http;
using System.Configuration;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Net.Http.Headers;

namespace InformationManagementSystem.Controllers
{
    public class RegisterUserController : ApiController
    {
        MongoContext _dbContext;

        [Obsolete]
        public RegisterUserController()
        {
            _dbContext = new MongoContext();
        }
        public IHttpActionResult GetUserDetails(RegisterUser reg)
        {
            var studDetails = _dbContext._database.GetCollection<RegisterUser>("RegisterUser").FindAll().ToList();
            return Json(studDetails);
        }

        // POST: api/RegisterUser
        [HttpPost]
        [Route("api/RegisterUser/EditRegList")]
        public IHttpActionResult Post(RegisterUser reg)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var document = _dbContext._database.GetCollection<BsonDocument>("RegisterUser");
            var query = Query.And(Query.EQ("UserName", reg.UserName), Query.EQ("EmailiD", reg.EmailiD));
            var count = document.FindAs<RegisterUser>(query).Count();
            var maxid = (from r in document.AsQueryable<RegisterUser>() select r.Id).Max();
            if (count == 0)
            {
                reg.Id =Double.Parse(maxid.ToString())+1;
                reg.UserRole = "Admin";
                var result = document.Insert(reg);
                ModelState.AddModelError(string.Empty, "Saved Successfully");
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }
}

// PUT: api/RegisterUser/5

    }

