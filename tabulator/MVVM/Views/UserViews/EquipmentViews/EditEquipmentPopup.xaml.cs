using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditEquipmentPopup.xaml
    /// </summary>
    public partial class EditEquipmentPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        EquipmentItem _equipmentToEdit;

        public EditEquipmentPopup(EquipmentItem equipmentToEdit)
        {
            InitializeComponent();
            _equipmentToEdit = equipmentToEdit;

            NameInput.Text = _equipmentToEdit.Name;
            DescriptionInput.Text = _equipmentToEdit.Description;

            if (_equipmentToEdit.Available)
                AvailableInput.SelectedIndex = 0;
            else AvailableInput.SelectedIndex = 1;
            AvailableInput_SelectionChanged(AvailableInput, null);
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
            RoomDataGrid.SelectedIndex = FindCurrentRoomIndex();
        }

        private int FindCurrentRoomIndex()
        {
            int roomID = _equipmentToEdit.Room.Id;

            foreach (var item in RoomDataGrid.Items)
            {
                var datagridRoomId = ((dynamic)item).ID;
                var datagridIndex = RoomDataGrid.Items.IndexOf(item);

                if(roomID.Equals(datagridRoomId))
                {
                    return datagridIndex;
                }
            }

            return -1;
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

        private void EditEquipment()
        {
            _equipmentToEdit.Name = NameInput.Text;
            _equipmentToEdit.Description = DescriptionInput.Text;
            _equipmentToEdit.Available = GetCheckboxValue(AvailableInput);
            _equipmentToEdit.NotInUse = GetCheckboxValue(NotInUseInput);
            _equipmentToEdit.Destroyed = GetCheckboxValue(DestroyedInput);
            _equipmentToEdit.Room = context.Rooms.ToList().Where(room => room.Id == (((dynamic)RoomDataGrid.SelectedItem).ID)).FirstOrDefault();
        }

        private bool GetCheckboxValue(ComboBox combobox)
        {
            string comboboxText = ((ComboBoxItem)combobox.SelectedItem)?.Content?.ToString();
            switch (comboboxText)
            {
                case "True":
                    return true;

                case "False":
                    return false;

                default:
                    return false;
            }
        }

        bool AllFieldsAreFilled()
        {
            if (NameInput.Text.Equals(string.Empty)) return false;
            if (DescriptionInput.Text.Equals(string.Empty)) return false;
            if (AvailableInput.SelectedIndex.Equals(-1)) return false;
            if (RoomDataGrid.SelectedIndex.Equals(-1)) return false;

            if (AvailableInput.SelectedIndex.Equals(1))
            {
                if (NotInUseInput.SelectedIndex.Equals(-1)) return false;
                if (DestroyedInput.SelectedIndex.Equals(-1)) return false;
            }

            return true;
        }

        void ClearAllFields()
        {
            NameInput.Text = string.Empty;
            DescriptionInput.Text = string.Empty;
            AvailableInput.SelectedIndex = 0;
            NotInUseInput.SelectedIndex = 0;
            DestroyedInput.SelectedIndex = 0;
            RoomDataGrid.SelectedIndex = 0;
            errorText.Text = string.Empty;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!AllFieldsAreFilled())
            {
                errorText.Text = "Fill out all fields!";
                return;
            }
            EditEquipment();

            context.SaveChanges();
            EditEquipmentDataView editEquipmentDataView = new EditEquipmentDataView();
            this.Close();
        }

        private void AvailableInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var available = ((ComboBoxItem)AvailableInput.SelectedItem)?.Content?.ToString();
            switch (available)
            {
                case "True":
                    NotInUseInput.Visibility = Visibility.Hidden;
                    DestroyedInput.Visibility = Visibility.Hidden;
                    notinuseText.Visibility = Visibility.Hidden;
                    destroyedText.Visibility = Visibility.Hidden;
                    break;

                case "False":
                    NotInUseInput.Visibility = Visibility.Visible;
                    destroyedText.Visibility = Visibility.Visible;
                    DestroyedInput.Visibility = Visibility.Visible;
                    notinuseText.Visibility = Visibility.Visible;
                    NotInUseInput.SelectedIndex = 0;
                    DestroyedInput.SelectedIndex = 1;
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
