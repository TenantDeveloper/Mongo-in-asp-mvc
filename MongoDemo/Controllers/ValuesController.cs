using MongoDB.Bson;
using MongoDB.Driver;
using MongoDemo.DbContext;
using MongoDemo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MongoDemo.Controllers
{
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        readonly MongoContext mongoContext = new MongoContext("IJsoorDemo");

      
        // GET api/values/5
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult Get()
        {
          IEnumerable<BsonDocument> bsonElements =   mongoContext.Read("services", new BsonDocument(new BsonElement("age", 26)));
            return Ok(bsonElements);
        }

        [Route("GetSpecificFeild")]
        [HttpGet]
        public IHttpActionResult GetSpecificField()
        {
          IEnumerable<BsonDocument> bsonElements =   mongoContext.GetSpecificFieldInCollection("employee");
            return Ok(bsonElements);
        }

        // POST api/values
        [Obsolete]
        public void Post([FromBody] ServiceDto serviceDto)
        {
            var document = new BsonDocument
{
           { "_Id", mongoContext.GetSpecificCollection("services").Find(new BsonDocument { }).Count()+ 1  }, //AutoIncreament
           { "name", serviceDto.Name },
    { "form", new BsonDocument { {"_Id", serviceDto.FormId } } },
    { "eligibility", new BsonDocument { { "Gender", (int)serviceDto.eligibility.Gender }, { "Nationality","All" }, { "Grade",(int)serviceDto.eligibility.Grade } } }
};
            mongoContext.Create("services", document);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]ServiceDto serviceDto)
        {
            BsonDocument bsonElements = mongoContext.GetById("services",id);
            var document = new BsonDocument
{
           { "_Id", bsonElements.GetValue("_Id")  }, //AutoIncreament
           { "name", serviceDto.Name },
    { "form", new BsonDocument { {"_Id", serviceDto.FormId } } },
    { "eligibility", new BsonDocument { { "Gender", (int)serviceDto.eligibility.Gender }, { "Nationality","All" }, { "Grade",(int)serviceDto.eligibility.Grade } } }
};
            mongoContext.Update("services", bsonElements, document);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            BsonDocument bsonElements = mongoContext.GetById("services", id);
            mongoContext.Delete("services", bsonElements);
        }
    }
}
