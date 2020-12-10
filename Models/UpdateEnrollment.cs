using System.ComponentModel.DataAnnotations;

namespace MyWebapiDemo.Models
{
    public class UpdateEnrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Grade { get; set; }
    }
}