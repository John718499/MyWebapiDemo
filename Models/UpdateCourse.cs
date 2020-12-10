using System.ComponentModel.DataAnnotations;

namespace MyWebapiDemo.Models
{
    public class UpdateCourse
    {
        [MaxLength(50)]
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}