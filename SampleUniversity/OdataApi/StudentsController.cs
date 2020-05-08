using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleUniversity.Data;
using SampleUniversity.Model;

namespace SampleUniversity.OdataApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context) => _context = context;

        [HttpGet] // šis iespējo vienkāršu REST pieprasījumu; GET: api/Students 
        [EnableQuery] // šis iespējo OData sintakses vaicājumus
        [UseSelection] // šis iespējo GraphQL lauku izvēli
        public IQueryable<StudentSearchResult> GetStudents([FromServices] UniversityContext c)
        {
            var result = new List<StudentSearchResult>();
            
            foreach (var student in c.Students)
            {
                var favoriteRepositories = GitHubODataClient.GetRepositoryInfo(student.FirstMidName).Result;

                result.Add(new StudentSearchResult(student, favoriteRepositories.Items));
            }
            return result.AsQueryable();
        } 

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSearchResult>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            var favoriteRepositories = await GitHubODataClient.GetRepositoryInfo(student.FirstMidName);

            return new StudentSearchResult(student, favoriteRepositories.Items);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.ID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.ID }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
