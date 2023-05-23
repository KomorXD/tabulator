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
using tabulator.MVVM.Views.AdminViews;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditDepartmentView.xaml
    /// </summary>
    public partial class EditDepartmentView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        public static DataGrid dataGrid;
        public EditDepartmentView()
        {
            InitializeComponent();
            DepartmentUserGrid.ItemsSource = context.Departments.ToList();
            dataGrid = DepartmentUserGrid;
        }
            
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (DepartmentUserGrid.SelectedItem as Department).Id;
            EditDepartmentPopup editDepartment = new EditDepartmentPopup(ID);
            editDepartment.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int ID = (DepartmentUserGrid.SelectedItem as Department).Id;
            var deleteDepartment = context.Departments.Where(m => m.Id == ID).Single();
            context.Departments.Remove(deleteDepartment);
            context.SaveChanges();
            DepartmentUserGrid.ItemsSource = context.Departments.ToList();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Departments.Where(x => x.Name.Contains(SearchTextBox.Text)).ToList();
            DepartmentUserGrid.ItemsSource = result;
        }
    }
}
