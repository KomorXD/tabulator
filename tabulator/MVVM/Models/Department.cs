﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.MVVM.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }

        [InverseProperty("Departments")]
        public virtual Faculty Faculty { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual ICollection<DepartmentRoom> Rooms { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual ICollection<DepartmentTechEmployee> TechEmployees { get; set; }
    }
}
