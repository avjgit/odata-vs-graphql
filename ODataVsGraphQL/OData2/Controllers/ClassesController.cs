using System.Collections.Generic;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData2.Models;

namespace OData2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        public class ProductsController : ODataController
        {
            private List<Course> courses = new List<Course>()
            {
                new Course()
                {
                    Id = 1,
                    Name = "Algorithm",
                }
            };

            [EnableQuery]
            public List<Course> Get()
            {
                return courses;
            }
        }
    }
}
