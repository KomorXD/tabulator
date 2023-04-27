using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.Models
{
    [Table("Faculties")]
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Address { get; set; }
        
        public virtual ICollection<Department> Departments { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual ICollection<FacultyRoom> FacultyRooms { get; set; }
    }
}
