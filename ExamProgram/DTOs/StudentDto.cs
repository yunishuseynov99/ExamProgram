using System.ComponentModel.DataAnnotations;

namespace ExamProgram.DTOs
{
    public class StudentDto
    {
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
