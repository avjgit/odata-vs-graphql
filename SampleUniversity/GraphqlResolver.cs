using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using SampleUniversity.Model;

namespace SampleUniversity
{
    [Route("graphql/")]
    public class GraphqlResolver
    {
        /// <summary>
        /// Data about students, enrollments and courses
        /// </summary>
        /// <param name="c">db context</param>
        /// <returns>Queried students</returns>
        [UseSelection]  // šis iespējo GraphQL lauku izvēli
        [UseFiltering]
        [UseSorting]
        public IQueryable<Student> GetStudents([Service]UniversityContext c) => c.Students;
    }
}
