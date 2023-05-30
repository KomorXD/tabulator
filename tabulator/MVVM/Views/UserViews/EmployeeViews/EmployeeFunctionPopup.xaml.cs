using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews.EmployeeViews
{
    /// <summary>
    /// Interaction logic for EmployeeFunctionPopup.xaml
    /// </summary>
    /// 

    public enum EmployeeFunc
    {
        None, Faculty, Department
    }

    public partial class EmployeeFunctionPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        string selectedOption;
        List<Faculty> _facultyList;
        List<Department> _departmentList;
        Employee _employee;
        EmployeeFunc _employeeFunc;

        public EmployeeFunctionPopup(Employee employee, EmployeeFunc function)
        {
            InitializeComponent();
            _facultyList = context.Faculties.ToList();
            _departmentList = context.Departments.ToList();
            _employee = employee;
            _employeeFunc = function;
            SelectFunction();
            ChangeCheckboxApperance();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            DeletePrevious();
            Edit();
            context.SaveChanges();
            this.Close();
        }

        private void DeletePrevious()
        {
            switch(_employeeFunc)
            {
                case EmployeeFunc.Faculty:
                    FacultyTechEmployee techFacultyEmployee = context.FacultyTechEmployee.FirstOrDefault(fac => fac.EmployeeId == _employee.Id);
                    techFacultyEmployee.Employee = _employee;
                    context.FacultyTechEmployee.Remove(techFacultyEmployee);
                    break;
                case EmployeeFunc.Department:
                    DepartmentTechEmployee techDepartmentEmployee = context.DepartmentTechEmployee.FirstOrDefault(dep => dep.EmployeeId == _employee.Id);
                    techDepartmentEmployee.Employee = _employee;
                    context.DepartmentTechEmployee.Remove(techDepartmentEmployee);
                    break;
            }
        }

        private void Edit()
        {
            selectedOption = ((ComboBoxItem)RoleDropdown.SelectedItem)?.Content?.ToString();
            switch (selectedOption)
            {
                case "Faculty technician":
                    FacultyTechEmployee techFacultyEmployee = new FacultyTechEmployee();
                    techFacultyEmployee.Employee = _employee;
                    techFacultyEmployee.Faculty = context.Faculties.ToList().Where(faculty => faculty.Name == ((dynamic)((ComboBoxItem)ItemTypeDropdown.SelectedItem)?.Content?.ToString())).FirstOrDefault();
                    context.FacultyTechEmployee.Add(techFacultyEmployee);
                    break;
                case "Department technician":
                    DepartmentTechEmployee techDepartmentEmployee = new DepartmentTechEmployee();
                    techDepartmentEmployee.Employee = _employee;
                    techDepartmentEmployee.Department = context.Departments.ToList().Where(department => department.Name == ((dynamic)((ComboBoxItem)ItemTypeDropdown.SelectedItem)?.Content?.ToString())).FirstOrDefault();
                    context.DepartmentTechEmployee.Add(techDepartmentEmployee);
                    break;
            }
        }

        private void FacultyDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeCheckboxApperance();
        }

        private void SelectFunction()
        {
            switch(_employeeFunc)
            {
               case EmployeeFunc.None:
                    RoleDropdown.SelectedIndex = 0;
               break;
               case EmployeeFunc.Department:
                    RoleDropdown.SelectedIndex = 2;
               break;
               case EmployeeFunc.Faculty: 
                    RoleDropdown.SelectedIndex = 1;
               break;
            }
        }

        private void ChangeCheckboxApperance()
        {
            selectedOption = ((ComboBoxItem)RoleDropdown.SelectedItem)?.Content?.ToString();

            switch (selectedOption)
            {
                case "Employee":
                    ItemTypeText.Visibility = Visibility.Hidden;
                    ItemTypeDropdown.Visibility = Visibility.Hidden;
                    break;
                case "Faculty technician":
                    if (ItemTypeText != null && ItemTypeDropdown != null)
                    {
                        ItemTypeText.Visibility = Visibility.Visible;
                        ItemTypeDropdown.Visibility = Visibility.Visible;
                    }

                    ItemTypeDropdown.Items.Clear();
                    for (int i = 0; i < _facultyList.Count; i++)
                    {
                        ItemTypeDropdown.Items.Add(new ComboBoxItem() { Content = _facultyList.ElementAt(i).Name });
                    }
                    ItemTypeDropdown.SelectedIndex = 0;

                    break;
                case "Department technician":
                    if (ItemTypeText != null && ItemTypeDropdown != null)
                    {
                        ItemTypeText.Visibility = Visibility.Visible;
                        ItemTypeDropdown.Visibility = Visibility.Visible;
                    }

                    ItemTypeDropdown.Items.Clear();
                    for (int i = 0; i < _departmentList.Count; i++)
                    {
                        ItemTypeDropdown.Items.Add(new ComboBoxItem() { Content = _departmentList.ElementAt(i).Name });
                    }
                    ItemTypeDropdown.SelectedIndex = 0;

                    break;
                default:
                    selectedOption = "";
                    if (ItemTypeText != null && ItemTypeDropdown != null)
                    {
                        ItemTypeText.Visibility = Visibility.Visible;
                        ItemTypeDropdown.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }
    }
}
