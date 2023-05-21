using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for EditUserDataView.xaml
    /// </summary>
    public partial class EditUserDataView : UserControl
    {
            DBContext context = DBContext.GetInstance();

        public EditUserDataView()
        {
            InitializeComponent();

            UserDataGrid.ItemsSource = context.Users.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Users.Where(x => x.Name.Contains(SearchTextBox.Text) || x.Surname.Contains(SearchTextBox.Text) || x.Username.Contains(SearchTextBox.Text)).ToList();
            UserDataGrid.ItemsSource = result;
        }
    }
}
