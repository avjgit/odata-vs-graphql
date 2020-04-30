using SampleUniversity.Data;
using SampleUniversity.Model;
using System;
using System.Linq;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UniversityContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
                new Student{FirstMidName="Cīrulis",LastName="Andrejs",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Student{FirstMidName="Rutka",LastName="Maija",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Osnovskis",LastName="Dmitrijs",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Student{FirstMidName="Liesma",LastName="Laima",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Bērziņa",LastName="Diāna",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Paraudziņš",LastName="Jānis",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Student{FirstMidName="Simenons",LastName="Žoržs",EnrollmentDate=DateTime.Parse("2018-09-01")},
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Chemistry",Credits=3},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
                new Course{CourseID=1045,Title="Calculus",Credits=4},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4},
                new Course{CourseID=2021,Title="Composition",Credits=3},
                new Course{CourseID=2042,Title="Literature",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=9},
                new Enrollment{StudentID=1,CourseID=4022,Grade=6},
                new Enrollment{StudentID=1,CourseID=4041,Grade=8},
                new Enrollment{StudentID=2,CourseID=1045,Grade=8},
                new Enrollment{StudentID=2,CourseID=3141,Grade=6},
                new Enrollment{StudentID=2,CourseID=2021,Grade=7},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=4022,Grade=5},
                new Enrollment{StudentID=5,CourseID=4041,Grade=6},
                new Enrollment{StudentID=6,CourseID=1045},
                new Enrollment{StudentID=7,CourseID=3141,Grade=9},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}