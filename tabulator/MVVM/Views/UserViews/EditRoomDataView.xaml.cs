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
        public static DataGrid dataGrid;
        List<Room> rooms;
        List<FacultyRoom> facultyRooms;
        List<DepartmentRoom> departmentRooms;
        public EditRoomDataView()
        {
            InitializeComponent();

            rooms = context.Rooms.ToList();
            facultyRooms = context.FacultyRooms.ToList();
            departmentRooms = context.DepartmentRooms.ToList();

            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, rooms, facultyRooms, departmentRooms);
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
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, rooms, facultyRooms, departmentRooms);
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Room> roomsTemp = context.Rooms.ToList().Where(r => r.Number.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            List<FacultyRoom> facultyRoomsTemp = context.FacultyRooms.ToList().Where(f => f.Room.Number.ToLower().Contains(SearchTextBox.Text.ToLower()) || f.Faculty.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            List<DepartmentRoom> departmentRoomsTemp = context.DepartmentRooms.ToList().Where(d => d.Room.Number.ToLower().Contains(SearchTextBox.Text.ToLower()) || d.Department.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            DataGridManager.GetInstance().ShowRoomsDataGrid(RoomDataGrid, roomsTemp, facultyRoomsTemp, departmentRoomsTemp);
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
