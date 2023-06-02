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
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;
using tabulator.MVVM.Viewmodels.UserVM;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditRoomPopup.xaml
    /// </summary>
    public partial class EditRoomPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        string selectedRoomType = "";
        List<Department> _departmentList;
        List<Faculty> _facultyList;
        public AddRoomViewModel ViewModel { get; set; }
        Room _roomToEdit;

        public EditRoomPopup(Room roomToEdit)
        {
            InitializeComponent();
            _roomToEdit = roomToEdit;
            _facultyList = context.Faculties.ToList();
            _departmentList = context.Departments.ToList();
            ViewModel = new AddRoomViewModel();

            RoomTypeDropdownSelectionChanged(null, null);
            RoomNumberInput.Text = _roomToEdit.Number;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _roomToEdit.Number = RoomNumberInput.Text;

            if(RoomNumberInput.Text == string.Empty)
            {
                errorText.Text = "Fill all fields!";
                return;
            }

            switch(selectedRoomType)
            {
                case "Faculty":
                    {
                        Faculty tempFaculty = context.Faculties.ToList().Where(faculty => faculty.Name == (((dynamic)((ComboBoxItem)RoomTypeItemDropdown.SelectedItem)?.Content?.ToString()))).FirstOrDefault();
                        ViewModel.ChangeToFacultyRoom(_roomToEdit, tempFaculty);

                        break;
                    }
                case "Department":
                    {
                        Department tempDepartment = context.Departments.ToList().Where(department => department.Name == (((dynamic)((ComboBoxItem)RoomTypeItemDropdown.SelectedItem)?.Content?.ToString()))).FirstOrDefault();
                        ViewModel.ChangeToDepartmentRoom(_roomToEdit, tempDepartment);

                        break;
                    }
            }
            context.SaveChanges();
            this.Close();
        }

        private void RoomTypeDropdownSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRoomType = ((ComboBoxItem)RoomTypeDropdown.SelectedItem)?.Content?.ToString();
            switch (selectedRoomType)
            {
                case "Faculty":
                    RoomTypeText.Text = "Faculty";
                    RoomTypeItemDropdown.Items.Clear();
                    foreach (Faculty faculty in _facultyList)
                    {
                        RoomTypeItemDropdown.Items.Add(new ComboBoxItem() { Content = faculty.Name });
                    }
                    RoomTypeItemDropdown.SelectedIndex = 0;
                    break;
                case "Department":
                    RoomTypeText.Text = "Department";
                    RoomTypeItemDropdown.Items.Clear();
                    foreach(Department department in _departmentList)
                    {
                        RoomTypeItemDropdown.Items.Add(new ComboBoxItem() { Content = department.Name });
                    }
                    RoomTypeItemDropdown.SelectedIndex = 0;
                    break;
            }
        }

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}
