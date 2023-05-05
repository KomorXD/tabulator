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
using tabulator.Models;
using tabulator.VVM.Viewmodels;

namespace tabulator.VVM.Views
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
            xd.Username = NameInput.Text;
            xd.Password = SurnameInput.Text;
            xd.FunctionId = UserFunction.ADMIN_ROLE;
      
            ViewModel.AddPerson(xd);

            btnLogin.Content = xd.Username;
        }
    }
}
