using InformationManagementSystem.App_Start;
using InformationManagementSystem.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InformationManagementSystem.Controllers
{
    public class CallListController : ApiController
    {
        MongoContext _dbContext;

        public BsonValue MobNo { get; private set; }

        [Obsolete]
        public CallListController()
        {
            _dbContext = new MongoContext();
        }
       
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        [Route("api/CallList/GetCallList")]
        public IHttpActionResult GetList(double UserID)
        {
            var document = _dbContext._database.GetCollection<BsonDocument>("CallList");
            //var callListDetail = document.FindAs<CallList>(Query.EQ("UserID", UserID)).ToList();         
            var callListDetail =
                            (from e in document.AsQueryable<CallList>()
                             .Where(x => x.UserID == UserID)
                             .OrderBy(x => x.Name)
                             select e
                             );
            //return Json(callListDetail);
            return Ok(callListDetail);
        }
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPost]
        [Route("api/CallList/SaveCallList")]
        public IHttpActionResult PostCallList(CallList cll)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            var document = _dbContext._database.GetCollection<BsonDocument>("CallList");
            var query = Query.And(Query.EQ("Name", cll.Name), Query.EQ("MobNo", cll.MobNo));
            var count = document.FindAs<CallList>(query).Count();
            var maxid = (from r in document.AsQueryable<CallList>() select r._id).Max();
            if (count == 0)
            {
                cll.Id = Double.Parse(maxid.ToString()) + 1;
                var result = document.Insert(cll);
                ModelState.AddModelError(string.Empty, "Saved Successfully");
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        [Route("api/CallList/GetCalllistByid")]
        public IHttpActionResult GetCalllistwithid(int Id)
        {
            var doc = _dbContext._database.GetCollection<CallList>("CallList");
            var query = Query.And(Query.EQ("Id", Id));
            var count = doc.FindAs<CallList>(query).Count();
            if (count > 0)
            {
                var PersonObjectid = Query.And(Query.EQ("Id", Id));
                var personDetail = _dbContext._database.GetCollection<CallList>("CallList").FindOne(PersonObjectid);
                return Ok(personDetail);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpPut]
        [Route("api/CallList/EditCallList")]
        public IHttpActionResult EditCall(CallList cll)
        {
            var callListObjectId = Query.And(Query.EQ("_id", cll._id));
            var collection = _dbContext._database.GetCollection<CallList>("CallList");
            var result = collection.Update(callListObjectId, Update.Replace(cll));
            return Ok();
        }
    }
}

