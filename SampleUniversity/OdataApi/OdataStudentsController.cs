using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using SampleUniversity.Model;

namespace SampleUniversity.OdataApi
{
    public class OdataStudentsController : ODataController
    {
        [EnableQuery]
        public IQueryable<Student> Get([FromServices] UniversityContext c)
        {
            return c.Students;
        }
    }
}
