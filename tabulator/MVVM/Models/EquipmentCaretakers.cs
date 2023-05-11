using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.MVVM.Models
{
    [Table("EquipmentCaretakers")]
    public class EquipmentCaretakers
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Item")]
        public int EquipmentId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EquipmentItem Item { get; set; }
    }
}
