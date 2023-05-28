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

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
