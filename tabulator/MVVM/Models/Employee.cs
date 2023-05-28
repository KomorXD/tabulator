using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.MVVM.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Column]
        public string PESEL { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Surname { get; set; }

        [Column]
        public string PhoneNumber { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [InverseProperty("Employees")]
        public virtual Room Room { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual ICollection<DepartmentTechEmployee> DepartmentTechEmployees { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual ICollection<FacultyTechEmployee> FacultyTechEmployees { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual ICollection<EquipmentCaretakers> Items { get; set; }
    }
}
