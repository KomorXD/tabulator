using System;
using System.Collections;
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
    /// Interaction logic for EditRoomDataView.xaml
    /// </summary>
    /// 

    public struct RoomDataGridStruct
    {
        public Room room;
        public string facultyName;
        public string departmentName;
    }

    public partial class EditRoomDataView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        public EditRoomDataView()
        {
            InitializeComponent();
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Room tempRoom = context.Rooms.ToList().Where(room => room.Id == (((dynamic)RoomDataGrid.SelectedItem).ID)).FirstOrDefault();
            EditRoomPopup editRoom = new EditRoomPopup(tempRoom);
            editRoom.ShowDialog();
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Room tempRoom = context.Rooms.ToList().Where(room => room.Id == (((dynamic)RoomDataGrid.SelectedItem).ID)).FirstOrDefault();
            context.Rooms.Remove(tempRoom);
            context.SaveChanges();
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid);
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Room> roomsTemp = context.Rooms.ToList().Where(r => r.Number.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            List<FacultyRoom> facultyRoomsTemp = context.FacultyRooms.ToList().Where(f => f.Room.Number.ToLower().Contains(SearchTextBox.Text.ToLower()) || f.Faculty.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            List<DepartmentRoom> departmentRoomsTemp = context.DepartmentRooms.ToList().Where(d => d.Room.Number.ToLower().Contains(SearchTextBox.Text.ToLower()) || d.Department.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, roomsTemp, facultyRoomsTemp, departmentRoomsTemp);
        }
    }
}
