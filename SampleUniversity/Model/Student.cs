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

        [NotMapped]
        public IList<Repository> FavoriteRepositories
        {
            get => GitHubODataClient.GetRepositoryInfo(FirstMidName).Result.Items;
            set { }
        }

        [NotMapped]
        public Repository FavoriteRepository
        {
            get => GitHubODataClient.GetRepositoryInfo(FirstMidName).Result.Items.FirstOrDefault();
            set { }
        }

        [NotMapped]
        public Repository FavReps
        {
            get => new FavReps( ).FavRep;
            set { }
        }

    }
}
