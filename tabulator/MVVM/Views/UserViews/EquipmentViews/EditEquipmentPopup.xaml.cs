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
        int Id;

        public EditEquipmentPopup(int EquipmentID)
        {
            InitializeComponent();
            Id = EquipmentID;

            var editEquipment = (from m in context.Equipment where m.Id == EquipmentID select m).FirstOrDefault();
            NameInput.Text = editEquipment.Name;
            DescriptionInput.Text = editEquipment.Description;

            if (editEquipment.Available)
                AvailableInput.SelectedIndex = 0;
            else AvailableInput.SelectedIndex = 1;

            AvailableInput_SelectionChanged(AvailableInput, null);
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
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
            EquipmentItem equipment = (from m in context.Equipment where m.Id == Id select m).FirstOrDefault();
            equipment.Name = NameInput.Text;
            equipment.Description = DescriptionInput.Text;

            // do ogarniecia przez tylny koniec xd

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
    }
}
