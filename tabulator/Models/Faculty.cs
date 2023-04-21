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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class FacultyConfiguration : EntityTypeConfiguration<Faculty>
    {
        public FacultyConfiguration()
        {
            ToTable("Faculties");
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(256);
            Property(c => c.Address).IsRequired().HasMaxLength(256);
        }
    }
}
