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
        int _recordsFound = 0;
        bool _available;
        bool _notInUse;
        bool _destroyed;

        public ReportGeneratorView()
        {
            InitializeComponent();
            Available.IsChecked = true;
            ViewModel = new AddDepartmentViewModel();            
            AddDataToDropdowns();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateRecordsFoundText()
        {
            _recordsFound = context.Equipment.Where(eq => eq.Name.Contains(EquipmentInput.Text) && eq.Room.Number.Contains(RoomInput.Text) && eq.Available.Equals(_available) && eq.Destroyed.Equals(_destroyed) && eq.NotInUse.Equals(_notInUse)).ToList().Count;
            RecordsText.Text = "Records found: " + _recordsFound.ToString();
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
            UpdateRecordsFoundText();
        }
        private void DepartmentDropdown_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecordsFoundText();
        }
        private void NameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRecordsFoundText();
        }
        private void SurnameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRecordsFoundText();
        }
        private void RoomInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRecordsFoundText();
        }
        private void DepartmentDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecordsFoundText();
        }
        private void EquipmentInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRecordsFoundText();
        }
        private void Available_Checked(object sender, RoutedEventArgs e)
        {
            _available = true;
            UpdateRecordsFoundText();
        }
        private void Available_Unchecked(object sender, RoutedEventArgs e)
        {
            _available = false; 
            UpdateRecordsFoundText();
        }
        private void NotInUse_Checked(object sender, RoutedEventArgs e)
        {
            _notInUse = true;
            UpdateRecordsFoundText();
        }
        private void NotInUse_Unchecked(object sender, RoutedEventArgs e)
        {
            _notInUse = false;
            UpdateRecordsFoundText();
        }
        private void Destroyed_Checked(object sender, RoutedEventArgs e)
        {
            _destroyed = true;
            UpdateRecordsFoundText();
        }
        private void Destroyed_Unchecked(object sender, RoutedEventArgs e)
        {
            _destroyed = false;
            UpdateRecordsFoundText();
        }
    }
}
