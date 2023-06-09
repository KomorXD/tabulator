﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditDepartmentPopup.xaml
    /// </summary>
    public partial class EditDepartmentPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        int Id;
        List<Faculty> _facultyList;
        public EditDepartmentPopup(int DepartmentID)
        {
            InitializeComponent();
            Id = DepartmentID;

            Department editDepartment = (from m in context.Departments where m.Id == DepartmentID select m).FirstOrDefault();
            NameInput.Text = editDepartment.Name;
            _facultyList = context.Faculties.ToList();
            foreach (Faculty faculty in _facultyList)
            {
                FacultyDropdown.Items.Add(new ComboBoxItem() { Content = faculty.Name });
            }
            FacultyDropdown.SelectedIndex = GetCurrentFacultyIndex(editDepartment);
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

        int GetCurrentFacultyIndex(Department dep)
        {
            for (int i = 0; i < _facultyList.Count; i++)
            {
                if (dep.Faculty.Id.Equals(_facultyList.ElementAt(i).Id))
                    return i;
            }

            return 0;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(NameInput.Text == string.Empty)
            {
                errorText.Text = "Fill all fields!";
                return;
            }

            Department department = (from m in context.Departments where m.Id == Id select m).FirstOrDefault();
            department.Name = NameInput.Text;
            int selectedRole = FacultyDropdown.SelectedIndex;
            if(selectedRole == -1)
            {
                return;
            }
            department.Faculty = _facultyList.ElementAt(selectedRole);
            context.SaveChanges();
            this.Close();
        }

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            { 
                btnEdit_Click(sender, e);
            }
        }
    }
}
