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
            PasswordBox passwordBox = PasswordInput;
            addUser.Username = UsernameInput.Text;
            addUser.Name = NameInput.Text;
            addUser.Surname = SurnameInput.Text;
            addUser.Password = "xdddd";

            ComboBoxItem selectedObject = FunctionDropdown.SelectedItem as ComboBoxItem;
            if (selectedObject != null)
            {
                string selectedValue = selectedObject.ToString();
                if(selectedValue == "System.Windows.Controls.ComboBoxItem: User") 
                {
                    addUser.FunctionId = UserFunction.USER_ROLE;
                }
                if (selectedValue == "System.Windows.Controls.ComboBoxItem: Admin")
                {
                    addUser.FunctionId = UserFunction.ADMIN_ROLE;
                }
            }
            else
            {
                // No item is selected
            }

            ViewModel.AddPerson(addUser);
            ClearTextBoxes();
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
