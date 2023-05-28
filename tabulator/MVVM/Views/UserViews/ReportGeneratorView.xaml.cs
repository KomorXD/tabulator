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
using System.Xml.Linq;
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
        Faculty _selectedFaculty;
        Department _selectedDepartment;
        List<Department> _departmentList;
        List<EquipmentItem> _recordsFoundList;
        int _recordsFoundCount = 0;


        bool _facultyStopSearch = true;
        bool _departmentStopSearch = true;

        
        bool _available = true;
        bool _notInUse = false;
        bool _destroyed = false;

        public ReportGeneratorView()
        {
            InitializeComponent();
            Available.IsChecked = true;
            _facultyList = context.Faculties.ToList();
            ViewModel = new AddDepartmentViewModel();
            FillFacultyDropBoxes();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateRecordsFoundText()
        {
            List<EquipmentItem> allEquipment = context.Equipment.ToList();
            //EquipmentItem eq = allEquipment.ElementAt(0);
            //int warunek = 0;
            //if (eq.Available.Equals(_available)) warunek++;
            //if (eq.Destroyed.Equals(_destroyed)) warunek++;
            //if (eq.NotInUse.Equals(_notInUse)) warunek++;
            //if (InFacultyEquipmentSearch(eq)) warunek++;
            //if (InDepartmentEquipmentSearch(eq)) warunek++;
            //if (CaretakernNameEquipmentSearch(eq, NameInput.Text)) warunek++;
            //if (CaretakernSurnameEquipmentSearch(eq, SurnameInput.Text)) warunek++;
            //if (NameEquipmentSearch(eq, EquipmentInput.Text)) warunek++;
            //if (RoomEquipmentSearch(eq, RoomInput.Text)) warunek++;
            _recordsFoundList = allEquipment
                .Where(eq => eq.Available.Equals(_available) &&
                             eq.Destroyed.Equals(_destroyed) &&
                             eq.NotInUse.Equals(_notInUse) &&
                             InFacultyEquipmentSearch(eq) &&
                             InDepartmentEquipmentSearch(eq) &&
                             CaretakernNameEquipmentSearch(eq, NameInput.Text) &&
                             CaretakernSurnameEquipmentSearch(eq, SurnameInput.Text) &&
                             NameEquipmentSearch(eq, EquipmentInput.Text) &&
                             RoomEquipmentSearch(eq, RoomInput.Text))
                .ToList();
            _recordsFoundCount = _recordsFoundList.Count;
            RecordsText.Text = "Records found: " + _recordsFoundCount.ToString();
        }

        bool InFacultyEquipmentSearch(EquipmentItem eq)
        {
            //Dodać przeszukanie wszytskich departmentów w danym fakultecie

            //Wyłączyć szukanie jeżeli wybrany jest jakis department

            if (FacultyDropdown.SelectedIndex <= 0) return true;

            foreach (FacultyRoom facultyRoom in context.FacultyRooms)
            {
                if(eq.Room == facultyRoom.Room && _selectedFaculty == facultyRoom.Faculty)
                {
                    return true;
                }
            }

            return false;
        }
        bool InDepartmentEquipmentSearch(EquipmentItem eq)
        {
            if (DepartmentDropdown.SelectedIndex <= 0) return true;

            foreach (DepartmentRoom departmentRoom in context.DepartmentRooms)
            {
                if (eq.Room == departmentRoom.Room && _selectedDepartment == departmentRoom.Department)
                {
                    return true;
                }
            }

            return false;
        }

        bool CaretakernNameEquipmentSearch(EquipmentItem eq, string name)
        {
            if (name.Equals(string.Empty)) return true;

            foreach (EquipmentCaretakers equipmentCaretakers in context.EquipmentCaretakers)
            {
                if(eq == equipmentCaretakers.Item && equipmentCaretakers.Employee.Name.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }

        bool CaretakernSurnameEquipmentSearch(EquipmentItem eq, string surname)
        {
            if (surname.Equals(string.Empty)) return true;

            foreach (EquipmentCaretakers equipmentCaretakers in context.EquipmentCaretakers)
            {
                if (eq == equipmentCaretakers.Item && equipmentCaretakers.Employee.Surname.Contains(surname))
                {
                    return true;
                }
            }

            return false;
        }
        bool NameEquipmentSearch(EquipmentItem eq, string name)
        {
            if(name.Equals(string.Empty)) return true;

            if (eq.Name.Contains(name))
            {
                return true;
            }

            return false;
        }

        bool RoomEquipmentSearch(EquipmentItem eq, string number)
        {
            if (number.Equals(string.Empty)) return true;

            if (eq.Room.Number.Contains(number))
                return true;

            return false;
        }

        void FillFacultyDropBoxes()
        {
            FacultyDropdown.Items.Clear();
            FacultyDropdown.Items.Add(new ComboBoxItem() { Content = "None" });
            for (int i = 0; i < _facultyList.Count; i++)
            {
                FacultyDropdown.Items.Add(new ComboBoxItem() { Content = _facultyList.ElementAt(i).Name });
            }
            FacultyDropdown.SelectedIndex = 0;
        }

        private void FacultyDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecordsFoundText();
            FillDepartmentDropBox();
        }
        private void FillDepartmentDropBox()
        {         
            DepartmentDropdown.Items.Clear();
            DepartmentDropdown.Items.Add(new ComboBoxItem() { Content = "None" });
            DepartmentDropdown.SelectedIndex = 0;

            if (FacultyDropdown.SelectedIndex <= 0) return;

            Faculty tempFaculty = context.Faculties.ToList().Where(faculty => faculty.Name == ((dynamic)((ComboBoxItem)FacultyDropdown.SelectedItem)?.Content?.ToString())).FirstOrDefault();
            if (tempFaculty is null)
                return;
            _selectedFaculty = tempFaculty;

            List<Department> tempDepartmentList = context.Departments.Where(de => de.Faculty.Id.Equals(tempFaculty.Id)).ToList();
            for (int i = 0; i < tempDepartmentList.Count; i++)
            {
                DepartmentDropdown.Items.Add(new ComboBoxItem() { Content = tempDepartmentList.ElementAt(i).Name });
            }
        }

        private void DepartmentDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecordsFoundText();

            Department tempDepartment = context.Departments.ToList().Where(department => department.Name == ((dynamic)((ComboBoxItem)DepartmentDropdown.SelectedItem)?.Content?.ToString())).FirstOrDefault();
            if (tempDepartment is null)
                return;
            _selectedDepartment = tempDepartment;
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
        private void OnResetFiltersClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
