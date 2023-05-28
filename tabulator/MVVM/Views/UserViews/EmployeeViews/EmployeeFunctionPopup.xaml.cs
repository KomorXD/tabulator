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
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews.EmployeeViews
{
    /// <summary>
    /// Interaction logic for EmployeeFunctionPopup.xaml
    /// </summary>
    public partial class EmployeeFunctionPopup : Window
    {
        public EmployeeFunctionPopup(Employee employee)
        {
            InitializeComponent();
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

        }
    }
}
