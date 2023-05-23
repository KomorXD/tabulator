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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using tabulator.Core;
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
            User addUser = new User();

            addUser.Username = UsernameInput.Text;
            addUser.Name = NameInput.Text;
            addUser.Surname = SurnameInput.Text;
            addUser.Salt = PasswordManager.GenerateSalt();
            addUser.Password = PasswordManager.HashPassword(PasswordInput.Password, addUser.Salt);

            string selectedRole = ((ComboBoxItem)FunctionDropdown.SelectedItem)?.Content?.ToString();
            SelectRole(addUser, selectedRole);

            ViewModel.AddPerson(addUser);
            ClearTextBoxes();
        }

        private static void SelectRole(User addUser, string selectedRole)
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

        void ClearTextBoxes()
        {
            UsernameInput.Clear();
            NameInput.Clear();
            PasswordInput.Clear();
            SurnameInput.Clear();
        }
    }
}
