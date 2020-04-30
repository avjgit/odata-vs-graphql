using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using SampleUniversity.Data;
using SampleUniversity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUniversity.Controllers
{

    public class EnrollmentController
    {
        [HttpGet, EnableQuery]
        public IQueryable<Enrollment> Get([FromServices]UniversityContext context)
            => context.Enrollments;
    }

}
