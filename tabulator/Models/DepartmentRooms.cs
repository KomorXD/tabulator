using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.Models
{
    [Table("DepartmentRooms")]
    public class DepartmentRoom
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Room Room { get; set; }
    }
}
