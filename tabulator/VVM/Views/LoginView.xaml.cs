using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using tabulator.Models;
using tabulator.VVM.Views.AdminViews;

namespace tabulator.VVM.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void SwitchToAdminView(object sender, RoutedEventArgs e)
        {
            AdminView adminView = new AdminView();
            adminView.Show();
            this.Hide();
        }

        private void SwitchToUserView(object sender, RoutedEventArgs e)
        {
            UserView userView = new UserView();
            userView.Show();
            this.Hide();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //we se to ogarnijcie rzeby dzialalo
            string username = UsernameInput.Text;
            PasswordBox passwordBox = PasswordInput;
            SwitchToUserView(sender, e);
        }
    }
}
