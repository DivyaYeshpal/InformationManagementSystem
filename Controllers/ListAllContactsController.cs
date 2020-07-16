using InformationManagementSystem.App_Start;
using InformationManagementSystem.Models;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using NSubstitute.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace InformationManagementSystem.Controllers
{
    public class ListAllContactsController : ApiController
    {
        MongoContext _dbContext;

        [Obsolete]
        public ListAllContactsController()
        {
            _dbContext = new MongoContext();
        }
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [HttpGet]
        [Route("api/ListAllContacts/GetCallList")]
        public IHttpActionResult GetList(int pageNumber)
        {
            //int pageSize = 10;
            var collection = _dbContext._database.GetCollection<CallList>("CallList");
            var callListDetail =
                        (from e in collection.AsQueryable<CallList>()
                         .OrderBy(x => x.Name)
                         select e);
            ////var source = (from customer in _context.CustomerTBs.
            ////       OrderBy(a => a.Country)
            ////              select customer).AsQueryable();
            //// Get's No of Rows Count   
            //int count = callListDetail.Count();

            //// Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            //int CurrentPage = pageNumber;

            //// Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            //int PageSize = pageSize;

            //// Display TotalCount to Records to User  
            //int TotalCount = count;

            //// Calculating Totalpage by Dividing (No of Records / Pagesize)  
            //int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            //// Returns List of Customer after applying Paging   
            //var items = callListDetail.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            //// if CurrentPage is greater than 1 means it has previousPage  
            //var previousPage = CurrentPage > 1 ? "Yes" : "No";

            //// if TotalPages is greater than CurrentPage means it has nextPage  
            //var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            //// Object which we are going to send in header   
            //var paginationMetadata = new
            //{
            //    totalCount = TotalCount,
            //    pageSize = PageSize,
            //    currentPage = CurrentPage,
            //    totalPages = TotalPages,
            //    previousPage,
            //    nextPage
            //};

            //// Setting Header  
            //HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            //// Returing List of Customers Collections  
            ////return items;
            //return Ok(items);
            return Ok(callListDetail);
        }
    }
}
