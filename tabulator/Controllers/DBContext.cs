﻿using System.Data.Entity;
using tabulator.MVVM.Models;

namespace tabulator.DatabaseContext
{
    internal class DBContext : DbContext
    {
        static public DBContext INSTANCE = null;

        private DBContext(): base(Properties.Settings.Default.db_connect_str)
        {

        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EquipmentItem> Equipment { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFunction> UserFunctions { get; set; }

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
