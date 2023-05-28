using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private DBContext context = DBContext.GetInstance();

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
                ViewModel.AddUser(addUser);
            }

            ClearTextBoxes();
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

            if (PasswordInput.Password.Equals(string.Empty))
            {
                SetWarning(msg: "Fill password field.");
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
            addUser.Password = PasswordManager.HashPassword(PasswordInput.Password, addUser.Salt);

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
    }
}
