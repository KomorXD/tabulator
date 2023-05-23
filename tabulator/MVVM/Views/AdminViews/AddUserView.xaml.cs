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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;
using tabulator.MVVM.Viewmodels.AdminVM;

namespace tabulator.MVVM.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for AddUserView.xaml
    /// </summary>
    public partial class AddUserView : UserControl
    {
        public AddUserViewModel ViewModel { get; set; }
        public AddUserView()
        {
            InitializeComponent();
            ViewModel = new AddUserViewModel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SetWarning(msg: string.Empty);

            User addUser = new User();
            GetUserInfo(addUser);

            if (ValidateUser(addUser))
            {
                ViewModel.AddPerson(addUser);
            }

            ClearTextBoxes();
        }

        private bool ValidateUser(User addUser)
        {
            if (ManyTextBoxesEmpty())
            {
                SetWarning(msg: "Uzupelnij wszy pola");
                return false;
            }

            if (UsernameInput.Text.Equals(string.Empty))
            {
                SetWarning(msg: "Pusty username");
                return false;
            }

            if (PasswordInput.Password.Equals(string.Empty))
            {
                SetWarning(msg: "Pusty pass");
                return false;
            }

            if (NameInput.Text.Equals(string.Empty))
            {
                SetWarning(msg: "Pusty name");
                return false;
            }

            if (SurnameInput.Text.Equals(string.Empty))
            {
                SetWarning(msg: "Pusty sur");
                return false;
            }

            DBContext context = DBContext.GetInstance();

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
            addUser.Password = PasswordManager.HashPassword(PasswordInput.Password, addUser.Salt);
            string selectedRole = ((ComboBoxItem)FunctionDropdown.SelectedItem)?.Content?.ToString();
            SelectRole(addUser, selectedRole);
        }

        private void SelectRole(User addUser, string selectedRole)
        {
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
            if (PasswordInput.Password.Equals(string.Empty)) textBoxEmptyCount++;
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
            PasswordInput.Clear();
            SurnameInput.Clear();
        }

        void SetWarning(string msg)
        {
            errorPlaceholder.Text = msg;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
