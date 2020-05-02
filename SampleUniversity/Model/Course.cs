using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleUniversity.Model
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Id")]
        public int CourseID { get; set; }
        
        [DisplayName("Nosaukums")]
        public string Title { get; set; }

        [DisplayName("Kredītpunkti")]
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
