using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleUniversity.Model
{
    public class Student
    {
        public int ID { get; set; }

        [DisplayName("Uzvārds")]
        public string LastName { get; set; }
        
        [DisplayName("Vārds")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Reģistrācijas datums")]
        public DateTime EnrollmentDate { get; set; }

        [DisplayName("Reģistrācijas kursiem")]
        [UseFiltering]
        public ICollection<Enrollment> Enrollments { get; set; }

    }

    public class StudentSearchResult
    {

        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        
        public ICollection<Repository> FavoriteRepositories { get; set; }

        public StudentSearchResult(Student s, ICollection<Repository> repos)
        {
            ID = s.ID;
            LastName = s.LastName;
            FirstMidName = s.FirstMidName;
            EnrollmentDate = s.EnrollmentDate;
            Enrollments = s.Enrollments;
            FavoriteRepositories = repos;
        }

    }
}
