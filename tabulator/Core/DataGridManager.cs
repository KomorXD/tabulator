using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;
using tabulator.MVVM.Views.UserViews;

namespace tabulator.Core
{
    public class DataGridManager
    {
        DBContext context = DBContext.GetInstance();

        static private DataGridManager INSTANCE = null;
        static public DataGridManager GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new DataGridManager();
            }

            return INSTANCE;
        }

        public void ShowRoomsDataGrid(DataGrid dataGridToShow)
        {
            ShowRoomsDataGrid(dataGridToShow, context.Rooms.ToList(), context.FacultyRooms.ToList(), context.DepartmentRooms.ToList());
        }

        public void ShowRoomsDataGrid(DataGrid dataGridToShow, List<Room> rooms, List<FacultyRoom> facultyRooms, List<DepartmentRoom> departmentRooms)
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
                bool foundInAddedRooms = false;
                foreach (RoomDataGridStruct addedRoom in roomList)
                {
                    if (room.Id == addedRoom.room.Id)
                    {
                        foundInAddedRooms = true;
                        break;
                    }
                }

                if (!foundInAddedRooms)
                {
                    RoomDataGridStruct temp;
                    temp.room = room;
                    temp.facultyName = "-";
                    temp.departmentName = "-";
                    roomsWithoutAssigment.Add(temp);
                }
            }

            foreach (RoomDataGridStruct item in roomsWithoutAssigment)
            {
                roomList.Add(item);
            }

            roomList.Sort((r1, r2) => r1.room.Number.CompareTo(r2.room.Number));

            dataGridToShow.ItemsSource = roomList.Select(room => new
            {
                ID = room.room.Id,
                RoomName = room.room.Number,
                FacultyName = room.facultyName,
                DepartmentName = room.departmentName

            }).ToList();
        }

        public void ShowEquipmentDataGrid(DataGrid dataGridToShow, List<EquipmentItem> equipment)
        {
            dataGridToShow.ItemsSource = equipment.Select(eq => new
            {
                eq.Id,
                eq.Name,
                eq.Description,
                RoomNumber = eq.Room.Number,
                eq.Available,
                eq.NotInUse,
                eq.Destroyed
            }).ToList();
        }        
        
        public void ShowFacultiesDataGrid(DataGrid dataGridToShow, List<Faculty> faculties)
        {
            dataGridToShow.ItemsSource = faculties;
        }

        public void ShowShortEquipmentDataGrid(DataGrid dataGridToShow, List<EquipmentItem> eqList)
        {
            dataGridToShow.ItemsSource = eqList.Select(eq => new
            {
                ID = eq.Id,
                eq.Name,
                RoomNumber = eq.Room.Number,
                eq.Available
            }).Where(eq => eq.Available.Equals(true)).ToList();
        }
        public void ShowShortEmployeeDataGrid(DataGrid dataGridToShow, List<Employee> emplList)
        {
            dataGridToShow.ItemsSource = emplList.Select(empl => new
            {
                ID = empl.Id,
                empl.Name,
                empl.Surname
            }).ToList();
        }

    }
}
