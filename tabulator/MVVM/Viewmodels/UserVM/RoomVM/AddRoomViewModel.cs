﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Viewmodels.UserVM
{
    public class AddRoomViewModel
    {
        private ObservableCollection<Room> Rooms;
        private ObservableCollection<FacultyRoom> FacultyRooms;
        private ObservableCollection<DepartmentRoom> DepartmentRooms;
        DBContext context = DBContext.GetInstance();

        public AddRoomViewModel()
        {
            Rooms = new ObservableCollection<Room>(context.Rooms);
            FacultyRooms = new ObservableCollection<FacultyRoom>(context.FacultyRooms);
            DepartmentRooms = new ObservableCollection<DepartmentRoom>(context.DepartmentRooms);
        }

        private void AddRoom(Room room)
        {
            DBContext context = DBContext.GetInstance();
            Rooms.Add(room);
            context.Rooms.Add(room);
            context.SaveChanges();
        }

        public void ChangeToDepartmentRoom(Room room, Department department)
        {
            RemoveRoomFromFacultiesAndDepartments(room);
            DepartmentRoom temp = new DepartmentRoom();
            temp.Department = department;
            temp.Room = room;

            DepartmentRooms.Add(temp);
            context.DepartmentRooms.Add(temp);
            context.SaveChanges();
        }

        public void ChangeToFacultyRoom(Room room, Faculty faculty)
        {
            RemoveRoomFromFacultiesAndDepartments(room);
            FacultyRoom temp = new FacultyRoom();
            temp.Faculty = faculty;
            temp.Room = room;

            FacultyRooms.Add(temp);
            context.FacultyRooms.Add(temp);
            context.SaveChanges();
        }

        private void RemoveRoomFromFacultiesAndDepartments(Room room)
        {
            foreach (DepartmentRoom d in DepartmentRooms)
            {
                if (d.Room == room)
                {
                    DepartmentRooms.Remove(d);
                    context.DepartmentRooms.Remove(d);
                    context.SaveChanges();
                    break;
                }
            }

            foreach (FacultyRoom f in FacultyRooms)
            {
                if (f.Room == room)
                {
                    FacultyRooms.Remove(f);
                    context.FacultyRooms.Remove(f);
                    context.SaveChanges();
                    break;
                }
            }
        }

        public void AddFacultyRoom(Room room, Faculty faculty)
        {
            AddRoom(room);

            FacultyRoom facultyRoom = new FacultyRoom();
            facultyRoom.Room = room;
            facultyRoom.Faculty = faculty;

            DBContext context = DBContext.GetInstance();
            FacultyRooms.Add(facultyRoom);
            context.FacultyRooms.Add(facultyRoom);
            context.SaveChanges();
        }

        public void AddDepartmentRoom(Room room, Department department)
        {
            AddRoom(room);

            DepartmentRoom departmentRoom = new DepartmentRoom();
            departmentRoom.Room = room;
            departmentRoom.Department = department;

            DBContext context = DBContext.GetInstance();
            DepartmentRooms.Add(departmentRoom);
            context.DepartmentRooms.Add(departmentRoom);
            context.SaveChanges();
        }
    }

}
