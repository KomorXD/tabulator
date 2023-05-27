using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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
            List<Employee> result = context.Employees.ToList();
            if(!SearchTextBox.Text.Equals(string.Empty))
                result = context.Employees.Where(x => x.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) || x.Surname.ToLower().ToString().Contains(SearchTextBox.Text.ToLower()) || x.PESEL.ToLower().ToString().Contains(SearchTextBox.Text.ToLower()) || x.PhoneNumber.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
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
