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

        public EditDepartmentPopup(int DepartmentID)
        {
            InitializeComponent();
            Id = DepartmentID;

            var editDepartment = (from m in context.Departments where m.Id == DepartmentID select m).FirstOrDefault();
            NameInput.Text = editDepartment.Name;
            FacultyDropdown.Items.Add(new ComboBoxItem() { Content = "Faculty Example 1" });
            FacultyDropdown.Items.Add(new ComboBoxItem() { Content = "Faculty Example 2" });
            FacultyDropdown.SelectedIndex = 0;
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
            Department department = (from m in context.Departments where m.Id == Id select m).FirstOrDefault();
            department.Name = NameInput.Text;
            context.SaveChanges();
            EditDepartmentView.dataGrid.ItemsSource = context.Departments.ToList();
            this.Close();
        }
    }
}
