using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews.EmployeeViews
{
    /// <summary>
    /// Interaction logic for EmployeeFunction.xaml
    /// </summary>
    /// 

    struct EmployeeData
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
    }

    public partial class EmployeeFunction : UserControl
    {
        DBContext context = DBContext.GetInstance();

        public EmployeeFunction()
        {
            InitializeComponent();
            DataGridManager.GetInstance().ShowEmployeeFunction(FunctionsDataGrid, context.FacultyTechEmployee.ToList(), context.DepartmentTechEmployee.ToList(), context.Employees.ToList());
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<DepartmentTechEmployee> depResult = context.DepartmentTechEmployee.ToList();
            if (!SearchTextBox.Text.Equals(string.Empty))
            {
                depResult = context.DepartmentTechEmployee.Where(dep => dep.Department.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }
            List<FacultyTechEmployee> facResult = context.FacultyTechEmployee.ToList();
            if (!SearchTextBox.Text.Equals(string.Empty))
            {
                facResult = context.FacultyTechEmployee.Where(fac => fac.Faculty.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }
            List<Employee> empResult = context.Employees.ToList();
            if (!SearchTextBox.Text.Equals(string.Empty))
            {
                empResult = context.Employees.Where(emp => emp.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) || emp.Surname.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }
            DataGridManager.GetInstance().ShowEmployeeFunction(FunctionsDataGrid, facResult, depResult, empResult);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (dynamic)FunctionsDataGrid.SelectedItem;
            int id = selectedItem.Id;
            Employee employee = context.Employees.FirstOrDefault(x => x.Id == id);
            EmployeeFunctionPopup editEmployee = new EmployeeFunctionPopup(employee);
            editEmployee.ShowDialog();
            DataGridManager.GetInstance().ShowEmployeeFunction(FunctionsDataGrid, context.FacultyTechEmployee.ToList(), context.DepartmentTechEmployee.ToList(), context.Employees.ToList());
        }
    }
}
