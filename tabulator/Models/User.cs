using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tabulator.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column]
        public string Username { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Surname { get; set; }

        [ForeignKey("Function")]
        public int FunctionId { get; set; }

        public virtual UserFunction Function { get; set; }
    }
}
