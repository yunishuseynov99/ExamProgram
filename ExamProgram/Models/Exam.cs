using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamProgram.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Subject")]
        [Column(TypeName = "char(3)")]
        public string SubjectCode { get; set; }

        public Subject Subject { get; set; }

        [Required]
        [ForeignKey("Student")]
        [Range(1, 99999)]
        public int StudentNumber { get; set; }

        public Student Student { get; set; }

        [Required]
        public DateTime ExamDate { get; set; }

        [Range(0, 9)]
        public int Grade { get; set; }
    }
}
