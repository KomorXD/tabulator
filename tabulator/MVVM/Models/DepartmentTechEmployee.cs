using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.MVVM.Models
{
    [Table("DepartmentTechEmployee")]
    public class DepartmentTechEmployee
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Department Department { get; set; }
    }
}
