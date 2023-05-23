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
    /// Interaction logic for EditRoomDataView.xaml
    /// </summary>
    public partial class EditRoomDataView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        public static DataGrid dataGrid;
        public EditRoomDataView()
        {
            InitializeComponent();
            RoomDataGrid.ItemsSource = context.Rooms.ToList();
            dataGrid = RoomDataGrid;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (RoomDataGrid.SelectedItem as Room).Id;
            EditRoomPopup editRoom = new EditRoomPopup(ID);
            editRoom.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int ID = (RoomDataGrid.SelectedItem as Room).Id;
            var deleteRoom = context.Rooms.Where(m => m.Id == ID).Single();
            context.Rooms.Remove(deleteRoom);
            context.SaveChanges();
            RoomDataGrid.ItemsSource = context.Rooms.ToList();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = context.Rooms.Where(x => x.Number.ToString().Contains(SearchTextBox.Text)).ToList();
            RoomDataGrid.ItemsSource = result;
        }
    }
}
