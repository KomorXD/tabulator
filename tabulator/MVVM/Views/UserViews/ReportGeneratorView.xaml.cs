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
using System.Windows.Shapes;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;
using tabulator.MVVM.Viewmodels.UserVM;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for ReportGeneratorView.xaml
    /// </summary>
    public partial class ReportGeneratorView : UserControl
    {
        public AddDepartmentViewModel ViewModel { get; set; }
        DBContext context = DBContext.GetInstance();
        List<Faculty> _facultyList;
        int _selectedFaculty;
        List<Department> _departmentList;
        int recordsFound = 0;

        public ReportGeneratorView()
        {
            InitializeComponent();
            ViewModel = new AddDepartmentViewModel();            
            AddDataToDropdowns();

            UpdateText();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddDataToDropdowns()
        {
            if (_facultyList == null)
            {
                _facultyList = context.Faculties.ToList();
                FacultyDropdownSelection(null, null);
            }
            _selectedFaculty = _facultyList[FacultyDropdown.SelectedIndex].Id;
            _departmentList = context.Departments.Where(de => de.FacultyId == _selectedFaculty).ToList();
            DepartmentDropDownSelection(null, null);
        }
        private void FacultyDropdownSelection(object sender, SelectionChangedEventArgs e)
        {
            foreach (Faculty faculty in _facultyList)
            {
                FacultyDropdown.Items.Add(new ComboBoxItem() { Content = faculty.Name });
            }
            FacultyDropdown.SelectedIndex = 0;
        }
        private void DepartmentDropDownSelection(object sender, SelectionChangedEventArgs e)
        {
            DepartmentDropdown.Items.Clear();
            foreach (Department department in _departmentList)
            {
                DepartmentDropdown.Items.Add(new ComboBoxItem() { Content = department.Name });
            }
            DepartmentDropdown.SelectedIndex = 0;
        }
        private void FacultyDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddDataToDropdowns();
        }
        private void UpdateText()
        {
            RecordsText.Text = "Records found: " + recordsFound.ToString();
        }
    }
}
