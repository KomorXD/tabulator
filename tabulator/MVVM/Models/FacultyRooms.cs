using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.MVVM.Models
{
    [Table("FacultyRooms")]
    public class FacultyRoom
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual Room Room { get; set; }
    }
}
