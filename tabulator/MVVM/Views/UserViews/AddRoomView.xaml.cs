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
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddRoomView.xaml
    /// </summary>
    public partial class AddRoomView : UserControl
    {
        public AddRoomView()
        {
            InitializeComponent();
            RoomTypeDropdownSelectionChanged(null, null);
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            string roomNumber = RoomNumberInput.Text;
        }

        private void RoomTypeDropdownSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRoomType = ((ComboBoxItem)RoomTypeDropdown.SelectedItem)?.Content?.ToString();
            switch(selectedRoomType)
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

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
