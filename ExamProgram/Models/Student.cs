using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamProgram.Models
{
    public class Student
    {
        [Key]
        [Range(1, 99999)]
        public int StudentNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Range(1, 12)]
        public int Grade { get; set; }
    }
}
