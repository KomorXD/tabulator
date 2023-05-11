using System.Windows;
using System.Windows.Controls;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddFacultyView.xaml
    /// </summary>
    public partial class AddFacultyView : UserControl
    {
        public AddFacultyView()
        {
            InitializeComponent();
        }

        private void btnAddFaculty_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text;
            string address = AddressInput.Text;   
        }
    }
}
