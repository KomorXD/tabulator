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
using tabulator.Models;
using tabulator.VVM.Viewmodels.AdminVM;

namespace tabulator.VVM.Views.AdminViews
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
            User xd = new User();
            xd.Username = UsernameInput.Text;
            PasswordBox passwordBox = PasswordInput;
            xd.Name = NameInput.Text;
            xd.Surname = SurnameInput.Text;
            ComboBoxItem selectedObject = FunctionDropdown.SelectedItem as ComboBoxItem;

            if (selectedObject != null)
            {
                string selectedValue = selectedObject.ToString();
                if(selectedValue == "System.Windows.Controls.ComboBoxItem: User") 
                {
                    xd.FunctionId = UserFunction.USER_ROLE;
                }
                if (selectedValue == "System.Windows.Controls.ComboBoxItem: Admin")
                {
                    xd.FunctionId = UserFunction.ADMIN_ROLE;
                }
            }
            else
            {
                // No item is selected
            }

            ViewModel.AddPerson(xd);
        }
    }
}
