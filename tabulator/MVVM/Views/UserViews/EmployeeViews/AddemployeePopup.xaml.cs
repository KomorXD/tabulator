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
    /// Interaction logic for AddemployeePopup.xaml
    /// </summary>
    public partial class AddemployeePopup : Window
    {
        DBContext context = DBContext.GetInstance();
        string selectedOption;
        List<Faculty> _facultyList;
        List<Department> _departmentList;

        public AddemployeePopup(Employee employee)
        {
            InitializeComponent();
            ChangeCheckboxApperance();
            _facultyList = context.Faculties.ToList();
            _departmentList = context.Departments.ToList();

            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
            AddData(employee);
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

        private void RoleDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeCheckboxApperance();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!AllInputsFilled())
            {
                errorText.Text = "Fill all fields!";
                return;
            }

            Employee temp = new Employee();
            PopulateTempEquipment(temp);

            context.Employees.Add(temp);
            context.SaveChanges();

            selectedOption = ((ComboBoxItem)RoleDropdown.SelectedItem)?.Content?.ToString();
            switch (selectedOption)
            {
                case "Faculty technician":
                    FacultyTechEmployee techFacultyEmployee = new FacultyTechEmployee();
                    techFacultyEmployee.Employee = temp;
                    techFacultyEmployee.Faculty = context.Faculties.ToList().Where(faculty => faculty.Name == ((dynamic)((ComboBoxItem)ItemTypeDropdown.SelectedItem)?.Content?.ToString())).FirstOrDefault();
                    context.FacultyTechEmployee.Add(techFacultyEmployee);
                    context.SaveChanges();

                    break;
                case "Department technician":
                    DepartmentTechEmployee techDepartmentEmployee = new DepartmentTechEmployee();
                    techDepartmentEmployee.Employee = temp;
                    techDepartmentEmployee.Department = context.Departments.ToList().Where(department => department.Name == ((dynamic)((ComboBoxItem)ItemTypeDropdown.SelectedItem)?.Content?.ToString())).FirstOrDefault();
                    context.DepartmentTechEmployee.Add(techDepartmentEmployee);
                    context.SaveChanges();
                    break;
            }

            this.Close();
        }

        bool AllInputsFilled()
        {
            if (NameInput.Text.Equals(string.Empty)) return false;
            if (SurnameInput.Text.Equals(string.Empty)) return false;
            if (PeselInput.Text.Equals(string.Empty)) return false;
            if (RoleDropdown.SelectedIndex == -1) return false;
            if (RoomDataGrid.SelectedIndex == -1) return false;

            return true;
        }

        private void PopulateTempEquipment(Employee temp)
        {
            temp.Name = NameInput.Text;
            temp.Surname = SurnameInput.Text;
            temp.PESEL = PeselInput.Text;
            temp.PhoneNumber = PhoneNumberInput.Text;
            temp.Room = context.Rooms.ToList().Where(room => room.Id == (((dynamic)RoomDataGrid.SelectedItem).ID)).FirstOrDefault();
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

        private void AddData(Employee employee)
        {
            NameInput.Text = employee.Name;
            SurnameInput.Text = employee.Surname;
            PeselInput.Text = employee.PESEL;
            PhoneNumberInput.Text = employee.PhoneNumber;
        }
    }
}
