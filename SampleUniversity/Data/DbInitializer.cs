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
                new Student{FirstMidName="Andrejs",LastName="Cīrulis",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Student{FirstMidName="Maija",LastName="Rutka",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Dmitrijs",LastName="Osnovskis",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Student{FirstMidName="Laima",LastName="Liesma",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Diāna",LastName="Bērziņa",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Jānis",LastName="Paraudziņš",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Student{FirstMidName="Žoržs",LastName="Simenons",EnrollmentDate=DateTime.Parse("2018-09-01")}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=101,Title="Algoritmi",Credits=3},
                new Course{CourseID=201,Title="Bioinformātika",Credits=3},
                new Course{CourseID=301,Title="Civilaizsardzība",Credits=3},
                new Course{CourseID=401,Title="Dizains",Credits=4},
                new Course{CourseID=202,Title="Epidemioloģija",Credits=4},
                new Course{CourseID=999,Title="Fiziskā kultūra",Credits=3},
                new Course{CourseID=102,Title="Grafi",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=101,Grade=9},
                new Enrollment{StudentID=1,CourseID=201,Grade=6},
                new Enrollment{StudentID=1,CourseID=301,Grade=8},
                new Enrollment{StudentID=2,CourseID=401,Grade=8},
                new Enrollment{StudentID=2,CourseID=202,Grade=6},
                new Enrollment{StudentID=2,CourseID=999,Grade=7},
                new Enrollment{StudentID=3,CourseID=101},
                new Enrollment{StudentID=4,CourseID=101},
                new Enrollment{StudentID=4,CourseID=201,Grade=5},
                new Enrollment{StudentID=5,CourseID=301,Grade=6},
                new Enrollment{StudentID=6,CourseID=401},
                new Enrollment{StudentID=7,CourseID=202,Grade=9},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}