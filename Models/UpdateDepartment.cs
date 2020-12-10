using System.ComponentModel.DataAnnotations;

namespace MyWebapiDemo.Models
{
    public class UpdateDepartment
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}