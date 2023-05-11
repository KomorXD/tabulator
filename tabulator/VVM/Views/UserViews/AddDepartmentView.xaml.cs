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

namespace tabulator.VVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddDepartmentView.xaml
    /// </summary>
    public partial class AddDepartmentView : UserControl
    {
        public AddDepartmentView()
        {
            InitializeComponent();
            FacultyDropdownSelection(null, null);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text;
            string selectedFaculty = ((ComboBoxItem)FacultyDropdown.SelectedItem)?.Content?.ToString();
        }

        private void FacultyDropdownSelection(object sender, SelectionChangedEventArgs e)
        {
            FacultyDropdown.Items.Add(new ComboBoxItem() { Content = "Faculty Example 1" });
            FacultyDropdown.Items.Add(new ComboBoxItem() { Content = "Faculty Example 2" });
            FacultyDropdown.SelectedIndex = 0;
        }
    }
}
