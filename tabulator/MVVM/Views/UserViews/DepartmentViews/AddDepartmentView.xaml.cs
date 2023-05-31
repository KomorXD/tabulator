using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;
using tabulator.MVVM.Viewmodels.UserVM;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddDepartmentView.xaml
    /// </summary>
    public partial class AddDepartmentView : UserControl
    {
        public AddDepartmentViewModel ViewModel { get; set; }
        DBContext context = DBContext.GetInstance();
        List<Faculty> _facultyList;

        public AddDepartmentView()
        {
            InitializeComponent();
            ViewModel = new AddDepartmentViewModel();
            _facultyList = context.Faculties.ToList();

            FacultyDropdownSelection(null, null);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(NameInput.Text == string.Empty)
            {
                errorText.Text = "Fill all fields!";
                return;
            }

            Department department = new Department();
            department.Name = NameInput.Text;
            department.Faculty = _facultyList.ElementAt(FacultyDropdown.SelectedIndex);
            ViewModel.AddDepartment(department);
            ClearTextBoxes();           
        }

        private void ClearTextBoxes()
        {
            NameInput.Text = string.Empty;
            FacultyDropdown.SelectedIndex = 0;
            errorText.Text = string.Empty;
        }

        private void FacultyDropdownSelection(object sender, SelectionChangedEventArgs e)
        {
            foreach (Faculty faculty in _facultyList)
            {
                FacultyDropdown.Items.Add(new ComboBoxItem() { Content = faculty.Name });
            }
            FacultyDropdown.SelectedIndex = 0;
        }

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }
    }
}
