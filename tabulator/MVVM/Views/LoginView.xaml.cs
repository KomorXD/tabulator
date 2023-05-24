using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;
using tabulator.MVVM.Views.AdminViews;

namespace tabulator.MVVM.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private DBContext context;
        public LoginView()
        {
            InitializeComponent();
            context = DBContext.GetInstance();
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
            string username = UsernameInput.Text;
            User foundUser = context.Users.FirstOrDefault(u => u.Username == username);

            if (foundUser is null)
            {
                //warning
                return;
            }

            if (PasswordManager.VerifyPassword(PasswordInput.Password, foundUser.Password, foundUser.Salt))
            {
                if(foundUser.FunctionId == UserFunction.USER_ROLE)
                {
                    SwitchToUserView(sender, e);
                }
                else if(foundUser.FunctionId == UserFunction.ADMIN_ROLE)
                {
                    SwitchToAdminView(sender, e);
                }
            }
            else
            {
                //Warning
            }
           
        }
    }
}
