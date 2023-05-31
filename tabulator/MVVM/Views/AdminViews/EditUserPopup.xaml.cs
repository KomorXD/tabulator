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
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for EditUserPopup.xaml
    /// </summary>
    public partial class EditUserPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        int Id;

        public EditUserPopup(int UserID)
        {
            InitializeComponent();
            Id = UserID;

            var editUser = (from m in context.Users where m.Id == UserID select m).FirstOrDefault();
            UsernameInput.Text = editUser.Username;
            NameInput.Text = editUser.Name;
            SurnameInput.Text = editUser.Surname;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SetWarning(msg: string.Empty);

            User addUser = new User();
            GetUserInfo(addUser);

            if (ValidateUser(addUser))
            {
                User user = (from m in context.Users where m.Id == Id select m).FirstOrDefault();
                user.Name = NameInput.Text;
                user.Surname = SurnameInput.Text;
                user.Username = UsernameInput.Text;
                context.SaveChanges();
                EditUserDataView.dataGrid.ItemsSource = context.Users.ToList();
                this.Close();
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordPopup editPassword = new ChangePasswordPopup(Id);
            editPassword.ShowDialog();
        }

        private bool ValidateUser(User addUser)
        {
            if (ManyTextBoxesEmpty())
            {
                SetWarning(msg: "Fill all fields.");
                return false;
            }

            if (UsernameInput.Text.Equals(string.Empty))
            {
                SetWarning(msg: "Fill username field.");
                return false;
            }

            if (NameInput.Text.Equals(string.Empty))
            {
                SetWarning(msg: "Fill name field.");
                return false;
            }

            if (SurnameInput.Text.Equals(string.Empty))
            {
                SetWarning(msg: "Fill surname field.");
                return false;
            }

            bool isUsernameTaken = context.Users.Any(u => u.Username.Equals(addUser.Username));
            if (isUsernameTaken)
            {
                SetWarning(msg: "Username taken");
                return false;
            }

            return true;
        }

        private void GetUserInfo(User addUser)
        {
            addUser.Username = UsernameInput.Text;
            addUser.Name = NameInput.Text;
            addUser.Surname = SurnameInput.Text;
            addUser.Salt = PasswordManager.GenerateSalt();

            SelectRole(addUser);
        }

        private void SelectRole(User addUser)
        {
            string selectedRole = ((ComboBoxItem)FunctionDropdown.SelectedItem)?.Content?.ToString();
            switch (selectedRole)
            {
                case "User":
                    addUser.FunctionId = UserFunction.USER_ROLE;
                    break;
                case "Admin":
                    addUser.FunctionId = UserFunction.ADMIN_ROLE;
                    break;

                default:
                    addUser.FunctionId = UserFunction.USER_ROLE;
                    break;
            }
        }

        bool ManyTextBoxesEmpty()
        {
            int textBoxEmptyCount = 0;
            if (UsernameInput.Text.Equals(string.Empty)) textBoxEmptyCount++;
            if (NameInput.Text.Equals(string.Empty)) textBoxEmptyCount++;
            if (SurnameInput.Text.Equals(string.Empty)) textBoxEmptyCount++;

            if (textBoxEmptyCount > 1)
                return true;
            else
                return false;
        }

        void ClearTextBoxes()
        {
            UsernameInput.Clear();
            NameInput.Clear();
            SurnameInput.Clear();
        }

        void SetWarning(string msg)
        {
            errorPlaceholder.Text = msg;
        }

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}
