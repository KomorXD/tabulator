using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;
using tabulator.MVVM.Viewmodels.UserVM;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddRoomView.xaml
    /// </summary>
    public partial class AddRoomView : UserControl
    {
        public AddRoomViewModel ViewModel { get; set; }


        DBContext context = DBContext.GetInstance();
        List<Faculty> _facultyList;
        List<Department> _departmentList;

        public AddRoomView()
        {
            InitializeComponent();
            _facultyList = context.Faculties.ToList();
            _departmentList = context.Departments.ToList();
            ViewModel = new AddRoomViewModel();
            RoomTypeDropdownSelectionChanged(null, null);
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            if(RoomNumberInput.Text.Equals(string.Empty))
            {
                errorText.Text = "Fill all fields!";
                return;
            }

            var selectedRoomType = ((ComboBoxItem)RoomTypeDropdown.SelectedItem)?.Content?.ToString();
            Room newRoom = new Room();
            newRoom.Number = RoomNumberInput.Text;

            switch (selectedRoomType)
            {
                case "Faculty":
                    Faculty selectedFaculty = _facultyList.ElementAt(TypeNameDropdown.SelectedIndex);
                    ViewModel.AddFacultyRoom(newRoom, selectedFaculty);
                    break;

                case "Department":
                    Department selectedDepartment = _departmentList.ElementAt(TypeNameDropdown.SelectedIndex);
                    ViewModel.AddDepartmentRoom(newRoom, selectedDepartment);
                    break;
            }

            ClearFields();
        }

        private void RoomTypeDropdownSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRoomType = ((ComboBoxItem)RoomTypeDropdown.SelectedItem)?.Content?.ToString();

            switch (selectedRoomType)
            {
                case "Faculty":
                    RoomTypeText.Text = "Faculty";
                    TypeNameDropdown.Items.Clear();
                    for (int i = 0; i < _facultyList.Count; i++)
                    {                 
                        TypeNameDropdown.Items.Add(new ComboBoxItem() { Content = _facultyList.ElementAt(i).Name });
                    }
                    TypeNameDropdown.SelectedIndex = 0;

                    break;

                case "Department":
                    RoomTypeText.Text = "Department";
                    TypeNameDropdown.Items.Clear();
                    for (int i = 0; i < _facultyList.Count; i++)
                    {
                        TypeNameDropdown.Items.Add(new ComboBoxItem() { Content = _departmentList.ElementAt(i).Name });
                    }
                    TypeNameDropdown.SelectedIndex = 0;

                    break;
            }
        }

        private void ClearFields()
        {
            RoomNumberInput.Text = string.Empty;
            RoomTypeDropdown.SelectedIndex = 0;
            TypeNameDropdown.SelectedIndex = 0;
            errorText.Text = string.Empty;
        }

        private void Border_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                btnAddRoom_Click(sender, e);
            }
        }
    }
}
