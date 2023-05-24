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

namespace tabulator.MVVM.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for ChangePasswordPopup.xaml
    /// </summary>
    public partial class ChangePasswordPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        int Id;

        public ChangePasswordPopup(int id)
        {
            InitializeComponent();
            Id = id;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if(ValidatePassword())
            {
                User user = (from m in context.Users where m.Id == Id select m).FirstOrDefault();
                user.Password = Pass1Input.Password.ToString();
                context.SaveChanges();
                EditUserDataView.dataGrid.ItemsSource = context.Users.ToList();
                this.Close();
            }
            else
            {
                errorMsg.Visibility = Visibility.Visible;
            }
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

        private bool ValidatePassword()
        {
            if(Pass1Input.Password.ToString().Length != 0)
                return Pass1Input.Password.ToString() == Pass2Input.Password.ToString();
            return false;
        }
    }
}
