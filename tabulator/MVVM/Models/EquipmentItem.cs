using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.MVVM.Models
{
    [Table("Equipment")]
    public class EquipmentItem
    {
        [Key]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public bool Available { get; set; }

        [Column]
        public bool NotInUse { get; set; }

        [Column]
        public bool Destroyed { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [InverseProperty("Items")]
        public virtual Room Room { get; set; }

        [ForeignKey("EquipmentId")]
        public virtual ICollection<EquipmentCaretakers> Caretakers { get; set; }
    }
}
