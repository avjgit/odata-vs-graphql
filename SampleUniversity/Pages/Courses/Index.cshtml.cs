using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleUniversity.Data;
using SampleUniversity.Model;

namespace SampleUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly SampleUniversity.Data.UniversityContext _context;

        public IndexModel(SampleUniversity.Data.UniversityContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Courses.ToListAsync();
        }
    }
}
