﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleUniversity.Data;
using SampleUniversity.Model;

namespace SampleUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly SampleUniversity.Data.UniversityContext _context;

        public DetailsModel(SampleUniversity.Data.UniversityContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public SearchResult FavoriteRepositories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }

            FavoriteRepositories = await GitHubODataClient.GetRepositoryInfo(Student.FirstMidName);

            return Page();
        }
    }
}
