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

        [ForeignKey("StorageRoom")]
        public int StorageId { get; set; }
        
        public virtual Room StorageRoom { get; set; }
        
        public virtual ICollection<Department> Departments { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual ICollection<FacultyRoom> Rooms { get; set; }

        [ForeignKey("FacultyId")]
        public virtual ICollection<FacultyTechEmployee> TechEmployees { get; set; }
    }
}
