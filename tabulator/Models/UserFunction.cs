using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace tabulator.Models
{
    [Table("UserFunctions")]
    public class UserFunction
    {
        public static readonly int ADMIN_ROLE = 1;
        public static readonly int USER_ROLE = 2;

        [Key]
        public int Id { get; set; } 

        [Column]
        public string FunctionName { get; set; }
    }
}
