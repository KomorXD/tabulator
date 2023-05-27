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
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Views.UserViews
{
    /// <summary>
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>

    public partial class AddEmployeeView : UserControl
    {
        DBContext context = DBContext.GetInstance();
        public static DataGrid dataGrid;
        List<Room> rooms;
        List<FacultyRoom> facultyRooms;
        List<DepartmentRoom> departmentRooms;

        public AddEmployeeView()
        {
            InitializeComponent();
            rooms = context.Rooms.ToList();
            facultyRooms = context.FacultyRooms.ToList();
            departmentRooms = context.DepartmentRooms.ToList();
            
            ShowDataGrid();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowDataGrid()
        {
            List<RoomDataGridStruct> roomList = new List<RoomDataGridStruct>();
            foreach (FacultyRoom facultyRoom in facultyRooms)
            {
                RoomDataGridStruct temp;
                temp.room = facultyRoom.Room;
                temp.facultyName = facultyRoom.Faculty.Name;
                temp.departmentName = "-";
                roomList.Add(temp);
            }

            foreach (DepartmentRoom depatmentRoom in departmentRooms)
            {
                RoomDataGridStruct temp;
                temp.room = depatmentRoom.Room;
                temp.departmentName = depatmentRoom.Department.Name;
                temp.facultyName = "-";
                roomList.Add(temp);
            }

            List<RoomDataGridStruct> roomsWithoutAssigment = new List<RoomDataGridStruct>();

            foreach (Room room in rooms)
            {
                foreach (RoomDataGridStruct addedRoom in roomList)
                {
                    if (!room.Equals(addedRoom.room))
                    {
                        RoomDataGridStruct temp;
                        temp.room = room;
                        temp.facultyName = "-";
                        temp.departmentName = "-";
                        roomsWithoutAssigment.Add(temp);
                        break;
                    }
                }
            }

            foreach (RoomDataGridStruct item in roomsWithoutAssigment)
            {
                roomList.Add(item);
            }

            RoomDataGrid.ItemsSource = roomList.Select(room => new
            {
                RoomName = room.room.Number,
                FacultyName = room.facultyName,
                DepartmentName = room.departmentName

            }).ToList();

            dataGrid = RoomDataGrid;
        }
    }
}
