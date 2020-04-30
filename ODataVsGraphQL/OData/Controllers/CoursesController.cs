using System.Collections.Generic;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData.Models;

namespace OData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ODataController
    {
        private List<Course> courses = new List<Course>()
        {
            new Course()
            {
                ID = 1,
                Name = "Algorithms",
            }
        };

        [EnableQuery] // iespējo OData $expand, $filter uc.
        public List<Course> Get()
        {
            return courses;
        }
    }
}
