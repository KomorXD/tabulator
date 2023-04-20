using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.Models;

namespace tabulator.guwno
{
    internal class DBContext : DbContext
    {
        static public DBContext INSTANCE = null;

        private DBContext(): base(Properties.Settings.Default.db_connect_str)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FacultyConfiguration());
        }

        public DbSet<Faculty> Faculties { get; set; }

        static public DBContext GetInstance()
        {
            if(INSTANCE == null)
            {
                INSTANCE = new DBContext();
            }

            return INSTANCE;
        }
    }
}
