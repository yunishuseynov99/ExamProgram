using ExamProgram.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamProgram.DTOs
{
    public class ExamDto
    {  
        [Required]
        [Column(TypeName = "char(3)")]
        public string SubjectCode { get; set; }

        [Required]
        [Range(1, 99999)]
        public int StudentNumber { get; set; }

        [Required]
        public DateTime ExamDate { get; set; }

        [Range(0, 9)]
        public int Grade { get; set; }
    }
}
