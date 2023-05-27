using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Viewmodels.UserVM
{
    public class AddDepartmentViewModel
    {
        private ObservableCollection<Department> Departments;

        public AddDepartmentViewModel()
        {
            DBContext context = DBContext.GetInstance();
            Departments = new ObservableCollection<Department>(context.Departments);
        }
        public void AddDepartment(Department department)
        {
            DBContext context = DBContext.GetInstance();
            Departments.Add(department);
            context.Departments.Add(department);
            context.SaveChanges();
        }
    }
}
