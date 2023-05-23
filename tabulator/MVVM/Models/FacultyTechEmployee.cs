using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.MVVM.Models
{
    [Table("FacultyTechEmployee")]
    public class FacultyTechEmployee
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
