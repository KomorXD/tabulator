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
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;
using tabulator.MVVM.Viewmodels.UserVM;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for ReportGeneratorView.xaml
    /// </summary>
    /// 


    public partial class ReportGeneratorView : UserControl
    {
        public AddDepartmentViewModel ViewModel { get; set; }
        DBContext context = DBContext.GetInstance();
        List<Faculty> _facultyList;
        List<Department> _departmentList;
        List<FacultyRoom> _facultyRoomsList;
        List<DepartmentRoom> _departmentRoomsList;
        Faculty _selectedFaculty;
        Department _selectedDepartment;
        List<EquipmentItem> _recordsFoundList;
        int _recordsFoundCount = 0;
        
        bool _available = true;
        bool _notInUse = false;
        bool _destroyed = false;

        public ReportGeneratorView()
        {
            InitializeComponent();
            Available.IsChecked = true;
            _facultyList = context.Faculties.ToList();
            _departmentList = context.Departments.ToList();
            _departmentRoomsList = context.DepartmentRooms.ToList();
            _facultyRoomsList = context.FacultyRooms.ToList();
            
            ViewModel = new AddDepartmentViewModel();
            FillFacultyDropBoxes();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (_recordsFoundList.Count == 0)
                return;

            PdfCreator pdfCreator = new PdfCreator();
            RaportStats stats = GenerateStats();        

            pdfCreator.Generate(GeneratOutputList(), stats);
        }

        private List<OutputItemData> GeneratOutputList()
        {
            List<OutputItemData> tempList = new List<OutputItemData>();
            foreach (EquipmentItem record in _recordsFoundList)
            {
                OutputItemData temp;

                temp.itemName = record.Name;
                temp.itemDescription = record.Description;
                temp.owner = FindOwnerOfEquipment(record);
                temp.room = record.Room.Number;
                temp.site = FindSiteEquipment(record);
                temp.state = FindStateEquipment(record);

                tempList.Add(temp);
            }
            return tempList;
        }

        private string FindStateEquipment(EquipmentItem record)
        {
            string temp = string.Empty;

            if (record.Available)
                temp += "-Available\n";
            if (record.NotInUse)
                temp += "-Not in use\n";
            if (record.Destroyed)
                temp += "-Destroyed\n";

            if (temp.Equals(string.Empty))
                temp = "-";

            return temp;
        }

        private string FindOwnerOfEquipment(EquipmentItem eq)
        {
            foreach (EquipmentCaretakers caretaker in context.EquipmentCaretakers)
            {
                if (eq.Id == caretaker.Item.Id)
                    return caretaker.Employee.Name + " " + caretaker.Employee.Surname;
            }
            return "-";
        }

        private string FindSiteEquipment(EquipmentItem eq)
        {
            foreach (FacultyRoom facultyRoom in context.FacultyRooms)
            {
                if (facultyRoom.Room.Id == eq.Room.Id)
                    return facultyRoom.Faculty.Name + " - shared resource";
            }
            foreach (DepartmentRoom departmentRoom in context.DepartmentRooms)
            {
                if (departmentRoom.Room.Id == eq.Room.Id)
                    return departmentRoom.Department.Faculty.Name + " - " + departmentRoom.Department.Name;
            }

            return "-";
        }

        private RaportStats GenerateStats()
        {
            RaportStats temp;

            temp.employeeName = NameInput.Text.Equals(string.Empty) ? "-" : NameInput.Text;
            temp.employeeSurname = SurnameInput.Text.Equals(string.Empty) ? "-" : SurnameInput.Text;
            temp.roomNumber = RoomInput.Text.Equals(string.Empty) ? "-" : RoomInput.Text;
            temp.equipmentName = EquipmentInput.Text.Equals(string.Empty) ? "-" : EquipmentInput.Text;
            temp.faculty = _selectedFaculty == null ? "-" : _selectedFaculty.Name;
            temp.department = _selectedDepartment == null ? "-" : _selectedDepartment.Name;
            temp.available = _available ? "Yes" : "No";
            temp.notInUse = _notInUse ? "Yes" : "No";
            temp.destroyed = _destroyed ? "Yes" : "No";

            return temp;
        }

        private void UpdateRecordsFoundText()
        {
            List<EquipmentItem> allEquipment = context.Equipment.ToList();
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

            if (FacultyDropdown.SelectedIndex <= 0) return true;

            foreach (FacultyRoom facultyRoom in _facultyRoomsList)
            {
                if(eq.Room == facultyRoom.Room && _selectedFaculty == facultyRoom.Faculty)
                {
                    return true;
                }
                else if (_selectedFaculty == facultyRoom.Faculty && _selectedDepartment is null)
                {
                    foreach (DepartmentRoom departmentRoom in _departmentRoomsList)
                    {
                        if (eq.Room == departmentRoom.Room && _selectedFaculty == departmentRoom.Department.Faculty)
                        {
                            return true;
                        }
                    }
                }

            }

            return false;
        }
        bool InDepartmentEquipmentSearch(EquipmentItem eq)
        {
            if (DepartmentDropdown.SelectedIndex <= 0) return true;

            foreach (DepartmentRoom departmentRoom in _departmentRoomsList)
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
