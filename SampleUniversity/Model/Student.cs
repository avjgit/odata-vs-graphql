using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SampleUniversity.Model
{
    public class Student
    {
        public int ID { get; set; }

        [DisplayName("Uzvārds")] public string LastName { get; set; }

        [DisplayName("Vārds")] public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Reģistrācijas datums")]
        public DateTime EnrollmentDate { get; set; }

        [DisplayName("Reģistrācijas kursiem")]
        [UseFiltering]
        public ICollection<Enrollment> Enrollments { get; set; }

        [NotMapped]
        public Repository FavoriteRepositoryFromGitHubRest
        {
            get => GitHubODataClient.GetRepositoryInfo(FirstMidName).Result.Items.FirstOrDefault();
            set { }
        }

        [NotMapped]
        public Repository FavoriteRepositoryFromGitHubGraphQl
        {
            get => GitHubGraphQLClient.GetRepositoryInfo(FirstMidName).Result;
            set { }
        }
    }
}
