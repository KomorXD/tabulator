﻿using System;
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

            ShowDataGrid();
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

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
