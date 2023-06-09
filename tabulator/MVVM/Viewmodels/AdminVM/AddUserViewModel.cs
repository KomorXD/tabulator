﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.MVVM.Models;

namespace tabulator.MVVM.Viewmodels.AdminVM
{
    public class AddUserViewModel : ObservableObject
    {
        private ObservableCollection<User> Users;

        public AddUserViewModel()
        {
            DBContext context = DBContext.GetInstance();
            Users = new ObservableCollection<User>(context.Users);
        }

        public void AddUser(User user)
        {
            DBContext context = DBContext.GetInstance();
            Users.Add(user);
            context.Users.Add(user);
            context.SaveChanges();
        }

    }
}
