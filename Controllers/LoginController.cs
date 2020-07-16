using InformationManagementSystem.App_Start;
using InformationManagementSystem.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Web.Http;

namespace InformationManagementSystem.Controllers
{
    public class LoginController : ApiController
    {
        MongoContext _dbContext;

        [Obsolete]
        public LoginController()
        {
            _dbContext = new MongoContext();
        }
        //public IHttpActionResult GetUserDetails(RegisterUser reg)
        //{
        //    var studDetails = _dbContext._database.GetCollection<RegisterUser>("RegisterUser").FindAll().ToList();
        //    return Json(studDetails);
        //}

        // POST: api/RegisterUser
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        [Route("api/Login/LoginAllUsers")]
        public IHttpActionResult LoginPage2()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            //var studObjectid = Query.And(Query.EQ("StudentID", id));
            var Email = identity.Claims
                      .FirstOrDefault(c => c.Type == "Email").Value;
            var query = Query.And(Query.EQ("UserName", identity.Name), Query.EQ("EmailiD", Email));
            var collection = _dbContext._database.GetCollection<RegisterUser>("RegisterUser");
            var RegID =
                        from e in collection.AsQueryable<RegisterUser>()
                        where e.EmailiD == Email
                        select e;
            //double RID = double.Parse(RegID);
            return Ok(RegID);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        [Route("api/Login/LoginAdmins")]
        public IHttpActionResult LoginPage3()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var Email = identity.Claims
                      .FirstOrDefault(c => c.Type == "Email").Value;
            var UserName = identity.Name;
            return Ok("Hello " + UserName + ", Your Email ID is :" + Email);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        [Route("api/Login/LoginSuperAdmin")]
        public IHttpActionResult LoginPage1()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            //var studObjectid = Query.And(Query.EQ("StudentID", id));
            var Email = identity.Claims
                      .FirstOrDefault(c => c.Type == "Email").Value;
            var query = Query.And(Query.EQ("UserName", identity.Name), Query.EQ("EmailiD", Email));
            var collection = _dbContext._database.GetCollection<RegisterUser>("RegisterUser");
            var RegID =
                        from e in collection.AsQueryable<RegisterUser>()
                        where e.EmailiD == Email
                        select e;
            //double RID = double.Parse(RegID);
            return Ok(RegID);
            //return Ok("Hello " + identity.Name + "Your Role(s) are: " + string.Join(",", roles.ToList()));
        }
    }
}
