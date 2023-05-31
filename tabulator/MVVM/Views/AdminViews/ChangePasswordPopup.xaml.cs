using System.Linq;
using System.Windows;
using System.Windows.Input;
using tabulator.Core;
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
            errorMessage.Text = string.Empty;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if(ValidatePassword())
            {
                User user = (from m in context.Users where m.Id == Id select m).FirstOrDefault();
                ChangePassword(user);
                context.SaveChanges();
                EditUserDataView.dataGrid.ItemsSource = context.Users.ToList();
                Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
        private void ChangePassword(User user)
        {
            string newSalt = PasswordManager.GenerateSalt();
            string newPassword = PasswordManager.HashPassword(PasswordInput.Password, newSalt);

            user.Password = newPassword;
            user.Salt = newSalt;
        }

        private bool ValidatePassword()
        {
            errorMessage.Visibility = Visibility.Visible;

            if (PasswordInput.Password.ToString().Length == 0)
            {
                errorMessage.Text = "Fill password field.";
                return false;
            }

            if (PasswordRepeatedInput.Password.ToString().Length == 0)
            {
                errorMessage.Text = "Fill password repeated field.";
                return false;
            }

            if (PasswordInput.Password.ToString() != PasswordRepeatedInput.Password.ToString())
            {
                errorMessage.Text = "Passwords are not the same!";  
                return false;
            }

            return true;
        }

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnChange_Click(sender, e);
            }
        }
    }
}
