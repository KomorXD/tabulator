﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.MVVM.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Column]
        public string Number { get; set; }

        [ForeignKey("RoomId")]
        public virtual ICollection<DepartmentRoom> DepartmentRooms { get; set; }

        [ForeignKey("RoomId")]
        public virtual ICollection<FacultyRoom> FacultyRooms { get; set; }

        public virtual ICollection<EquipmentItem> Items { get; set; }
        
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
