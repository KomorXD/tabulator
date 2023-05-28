using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>

    public partial class AddEmployeeView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        string selectedOption;
        List<Faculty> _facultyList;
        List<Department> _departmentList;

        public AddEmployeeView()
        {
            InitializeComponent();
            ChangeCheckboxApperance();
            _facultyList = context.Faculties.ToList();
            _departmentList = context.Departments.ToList();

            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(!AllInputsFilled())
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

            ClearAllFields();
        }

        private void ClearAllFields()
        {
            NameInput.Text = string.Empty;
            SurnameInput.Text = string.Empty;
            PeselInput.Text = string.Empty;
            PhoneNumberInput.Text = string.Empty;
            RoomDataGrid.SelectedIndex = -1;
            RoleDropdown.SelectedIndex = 0;
            ItemTypeDropdown.SelectedIndex = -1;
        }

        private void PopulateTempEquipment(Employee temp)
        {
            temp.Name = NameInput.Text;
            temp.Surname = SurnameInput.Text;
            temp.PESEL = PeselInput.Text;
            temp.PhoneNumber = PhoneNumberInput.Text;
            temp.Room = context.Rooms.ToList().Where(room => room.Id == (((dynamic)RoomDataGrid.SelectedItem).ID)).FirstOrDefault();
        }

        bool AllInputsFilled()
        {
            if(NameInput.Text.Equals(string.Empty)) return false;
            if(SurnameInput.Text.Equals(string.Empty)) return false;
            if(PeselInput.Text.Equals(string.Empty)) return false;
            if(RoleDropdown.SelectedIndex == -1) return false;
            if(RoomDataGrid.SelectedIndex == -1) return false;

            return true;
        }

        private void OnDropDownChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeCheckboxApperance();
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
