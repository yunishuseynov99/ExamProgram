using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamProgram.Models
{
    public class Subject
    {
        [Key]
        [StringLength(3)]
        [Column(TypeName = "char(3)")]
        public string SubjectCode { get; set; }

        [Required]
        [StringLength(30)]
        public string SubjectName { get; set; }

        [Range(1, 12)]
        public int Grade { get; set; }

        [Required]
        [StringLength(20)]
        public string TeacherFirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string TeacherLastName { get; set; }
    }
}
