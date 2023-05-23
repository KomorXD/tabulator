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

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for EditRoomPopup.xaml
    /// </summary>
    public partial class EditRoomPopup : Window
    {
        DBContext context = DBContext.GetInstance();
        int Id;

        public EditRoomPopup(int RoomID)
        {
            InitializeComponent();
            RoomTypeDropdownSelectionChanged(null, null);
            Id = RoomID;

            var editRoom = (from m in context.Rooms where m.Id == RoomID select m).FirstOrDefault();
            RoomNumberInput.Text = editRoom.Number;
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
            Room room = (from m in context.Rooms where m.Id == Id select m).FirstOrDefault();
            room.Number = RoomNumberInput.Text;
            context.SaveChanges();
            EditRoomDataView.dataGrid.ItemsSource = context.Rooms.ToList();
            this.Close();
        }

        private void RoomTypeDropdownSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRoomType = ((ComboBoxItem)RoomTypeDropdown.SelectedItem)?.Content?.ToString();
            switch (selectedRoomType)
            {
                case "Faculty":
                    RoomTypeText.Text = "Faculty";
                    RoomTypeItemDropdown.Items.Clear();
                    //zastapic uzupelniaime danych z bazy danych xd
                    RoomTypeItemDropdown.Items.Add(new ComboBoxItem() { Content = "Faculty Example 1" });
                    RoomTypeItemDropdown.Items.Add(new ComboBoxItem() { Content = "Faculty Example 2" });
                    RoomTypeItemDropdown.SelectedIndex = 0;
                    break;

                case "Department":
                    RoomTypeText.Text = "Department";
                    RoomTypeItemDropdown.Items.Clear();
                    //zastapic uzupelniaime danych z bazy danych xd
                    RoomTypeItemDropdown.Items.Add(new ComboBoxItem() { Content = "Department Example 1" });
                    RoomTypeItemDropdown.Items.Add(new ComboBoxItem() { Content = "Department Example 2" });
                    RoomTypeItemDropdown.SelectedIndex = 0;
                    break;
            }
        }
    }
}
