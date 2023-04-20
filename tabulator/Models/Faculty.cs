using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabulator.Models
{
    public class Faculty
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class FacultyConfiguration : EntityTypeConfiguration<Faculty>
    {
        public FacultyConfiguration()
        {
            ToTable("Faculty");
            HasKey(c => c.id);
            Property(c => c.name).IsRequired().HasMaxLength(256);
            Property(c => c.address).IsRequired().HasMaxLength(128);
        }
    }
}
