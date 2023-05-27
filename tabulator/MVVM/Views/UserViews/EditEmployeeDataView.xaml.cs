using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    /// Interaction logic for EditEmployeeDataView.xaml
    /// </summary>
    public partial class EditEmployeeDataView : UserControl
    {
        DBContext context = DBContext.GetInstance();

        public EditEmployeeDataView()
        {
            InitializeComponent();
            AddDataToDataGrid(context.Employees.ToList());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Employees.Where(x => x.Name.Contains(SearchTextBox.Text) || x.Surname.ToString().Contains(SearchTextBox.Text) || x.PESEL.ToString().Contains(SearchTextBox.Text) || x.PhoneNumber.ToString().Contains(SearchTextBox.Text)).ToList();
            AddDataToDataGrid(result);
        }

        private void AddDataToDataGrid(List<Employee> emplList)
        {
            EmployeeDataGrid.ItemsSource = emplList.Select(empl => new
            {
                empl.Name,
                empl.Surname,
                empl.PESEL,
                empl.PhoneNumber
            }).ToList();
        }
    }
}
