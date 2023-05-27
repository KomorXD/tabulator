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
            rooms = context.Rooms.ToList();
            List<RoomDataGridStruct> roomList = new List<RoomDataGridStruct>();
            facultyRooms = context.FacultyRooms.ToList();
            departmentRooms = context.DepartmentRooms.ToList();
            foreach (FacultyRoom facultyRoom in facultyRooms)
            {
                RoomDataGridStruct temp;
                temp.roomName = facultyRoom.Room.Number;
                temp.facultyName = facultyRoom.Faculty.Name;
                temp.departmentName = "-";
                roomList.Add(temp);
            }

            foreach (DepartmentRoom depatmentRoom in departmentRooms)
            {
                RoomDataGridStruct temp;
                temp.roomName = depatmentRoom.Room.Number;
                temp.departmentName = depatmentRoom.Department.Name;
                temp.facultyName = "-";
                roomList.Add(temp);
            }

            RoomDataGrid.ItemsSource = roomList.Select(room => new
            {
                RoomName = room.roomName,
                FacultyName = room.facultyName,
                DepartmentName = room.departmentName

            }).ToList();
        }
    }
}
