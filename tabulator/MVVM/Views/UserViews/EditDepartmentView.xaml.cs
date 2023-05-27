﻿using System;
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
using tabulator.MVVM.Views.AdminViews;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditDepartmentView.xaml
    /// </summary>
    public partial class EditDepartmentView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        private List<Department> departments;

        public EditDepartmentView()
        {
            InitializeComponent();
            ShowDataGrid(context.Departments.ToList());
            departments = context.Departments.ToList();
        }
            
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (dynamic)DepartmentDataGrid.SelectedItem;
            int id = selectedItem.Id;
            EditDepartmentPopup editDepartment = new EditDepartmentPopup(id);
            editDepartment.ShowDialog();
            ShowDataGrid(context.Departments.ToList());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (dynamic)DepartmentDataGrid.SelectedItem;
            int id = selectedItem.Id;
            var deleteDepartment = context.Departments.Where(m => m.Id == id).Single();
            context.Departments.Remove(deleteDepartment);
            context.SaveChanges();
            ShowDataGrid(context.Departments.ToList());
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Departments.Where(x => x.Name.Contains(SearchTextBox.Text) || x.Faculty.Name.ToString().Contains(SearchTextBox.Text)).ToList();
            ShowDataGrid(result);
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowDataGrid(List<Department> departmentList)
        {
            DepartmentDataGrid.ItemsSource = departmentList.Select(dep => new
            {
                dep.Id,
                dep.Name,
                FacultyName = dep.Faculty.Name
            }).ToList();
        }
    }
}
