using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabulator.Core;
using tabulator.DatabaseContext;
using tabulator.Models;

namespace tabulator.VVM.Viewmodels
{
    public class AddUserViewModel : ObservableObject
    {
        private ObservableCollection<User> Users;

        public AddUserViewModel()
        {
            DBContext context = DBContext.GetInstance();
            Users = new ObservableCollection<User>(context.Users);
        }

        public void AddPerson(User user)
        {
            DBContext context = DBContext.GetInstance();
            Users.Add(user);
            context.Users.Add(user);
            context.SaveChanges();
        }

    }
}
