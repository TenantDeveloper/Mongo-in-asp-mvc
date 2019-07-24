using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDemo.Dto
{
    public class ServiceDto
    {
        public string Name { get; set; }
        public int FormId { get; set; }
        public Eligibility eligibility { get; set; }
    }
}